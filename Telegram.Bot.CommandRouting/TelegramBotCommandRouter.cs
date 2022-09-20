using System.Reflection;
using Telegram.Bot.CommandRouting.Attributes;
using Telegram.Bot.Types;

namespace Telegram.Bot.CommandRouting
{
    public class TelegramBotCommandRouter
    {
        private readonly IEnumerable<TelegramBotController> _controllers;
        private readonly IAuthorizationChatHandler _authorizationChatHandler;
        private readonly long _botId;

        public TelegramBotCommandRouter(
            IEnumerable<TelegramBotController> controllers,
            ITelegramBotClient client,
            IAuthorizationChatHandler authorizationChatHandler = null)
        {
            _controllers = controllers;
            _authorizationChatHandler = authorizationChatHandler;
            _botId = client.BotId.Value;
        }

        public async Task<bool> TryRunCommand(Update update)
        {
            foreach(var controller in _controllers)
            {
                var controllerType = controller.GetType();
                var controllerCustomAttributes = controllerType.GetCustomAttributes(false);

                if (IsControllerValidForUpdate(controllerCustomAttributes, update) == false)
                    continue;

                var authorizationAttribute = GetTelegramBotChatAuthorizedAttribute(controllerCustomAttributes);
                var commandMethods = GetControllersCommandMethods(controllerType);
                
                foreach(var method in commandMethods)
                {
                    if (IsCommandValidForUpdate(method, update) == false)
                        continue;
                    var parameters = method.GetParameters();
                    if (parameters.Length > 0 && IsParametersHasAnyFromUpdateAttribute(parameters) == false)
                        continue;
                    await CheckAuthorizaion(method, authorizationAttribute, update);
                    var parameterValues = GetParameterValues(parameters, update);
                    controller.TelegramUpdate = update;
                    await InvokeMethod(method, controller, parameterValues);
                    return true;
                }
            }
            return false;
        }

        private TelegramBotChatAuthorizedAttribute? GetTelegramBotChatAuthorizedAttribute(object[] controllerCustomAttributes)
        {
            var authorizationAttribute = controllerCustomAttributes
                                        .FirstOrDefault(c => c is TelegramBotChatAuthorizedAttribute)
                                        as TelegramBotChatAuthorizedAttribute;
            return authorizationAttribute;
        }

        private async Task InvokeMethod(MethodInfo method, TelegramBotController controller, object[] parameterValues)
        {
            var invokeResult = method.Invoke(controller, parameterValues);

            if (invokeResult is not null && method.ReturnType == typeof(Task))
            {
                await (Task)invokeResult;
            }
        }

        private bool IsParametersHasAnyFromUpdateAttribute(ParameterInfo[] parameters)
        {
            return parameters.Any(p => p.GetCustomAttributes(false).Any(c => c is FromUpdateAttribute));
        }

        private object[] GetParameterValues(ParameterInfo[] parameters, Update update)
        {
            var parameterValues = new object[parameters.Count()];
            var i = 0;
            foreach (var parameter in parameters)
            {
                var fromUpdateAttribute = parameter
                            .GetCustomAttributes(false)
                            .FirstOrDefault(c => c is FromUpdateAttribute)
                            as FromUpdateAttribute;

                object parameterValue = update;
                var property = default(PropertyInfo);

                foreach (var name in fromUpdateAttribute.Path)
                {
                    property = parameterValue.GetType().GetProperty(name);
                    parameterValue = property.GetValue(parameterValue);
                }

                if (parameter.ParameterType != typeof(string) && parameterValue is null ||
                    property.PropertyType != parameter.ParameterType)
                    throw new Exception();
                parameterValues[i++] = parameterValue;
            }

            return parameterValues;
        }

        private bool IsCommandValidForUpdate(MethodInfo method, Update update)
        {
            var methodCustomAttribute = method
                        .GetCustomAttributes(false)
                        .Where(a => a is TelegramBotCommandAttribute)
                        .Cast<TelegramBotCommandAttribute>();

            return methodCustomAttribute.Any(m => m.Validate(update, _botId));
        }

        private IEnumerable<MethodInfo> GetControllersCommandMethods(Type controllerType)
        {
            var methods = controllerType.GetMethods();
            var commandMethods = methods.Where(m => m.GetCustomAttributes(false).Any(a => a is TelegramBotCommandAttribute));
            return commandMethods;
        }

        private bool IsControllerValidForUpdate(object[] controllerCustomAttributes, Update update)
        {
            var telegramBotCommandAttributes = controllerCustomAttributes
                .Where(c => c is TelegramBotCommandAttribute)
                .Cast<TelegramBotCommandAttribute>();

            return telegramBotCommandAttributes.Any(c => c.Validate(update, _botId));
        }

        private async Task CheckAuthorizaion(
            MethodInfo method, 
            TelegramBotChatAuthorizedAttribute? authorizationAttribute, 
            Update update)
        {
            if (authorizationAttribute is not null)
            {
                var allowAnonymousAttribute = method
                    .GetCustomAttributes(false)
                    .Where(a => a is TelegramBotAllowAnonymousChatAttribute)
                    .FirstOrDefault();

                if (allowAnonymousAttribute == null)
                    if (_authorizationChatHandler != null)
                    {
                        var isAuthorized = await _authorizationChatHandler.CheckAuthorization(update);

                        if (isAuthorized == false)
                            throw new Exception();
                    }
                    else
                        throw new Exception("IAuthorizationChatHandler did not provided");
            }
        }
    }
}