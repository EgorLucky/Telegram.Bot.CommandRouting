using Telegram.Bot.CommandRouting.Attributes;
using Telegram.Bot.Types;

namespace Telegram.Bot.CommandRouting
{
    public class TelegramBotCommandRouter
    {
        private readonly IReadOnlyCollection<TelegramBotController> _controllers;
        private readonly IAuthorizationChatHandler _authorizationChatHandler;
        private readonly long _botId;

        public TelegramBotCommandRouter(
            IReadOnlyCollection<TelegramBotController> controllers,
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

                var telegramBotCommandAttributes = controllerCustomAttributes
                    .Where(c => c is TelegramBotCommandAttribute)
                    .Cast<TelegramBotCommandAttribute>();

                var notValidatedAttributes = telegramBotCommandAttributes.Where(c => c.Validate(update, _botId) == false);

                if (notValidatedAttributes.Count() > 0)
                    continue;

                var authorizationAttribute = controllerCustomAttributes.FirstOrDefault(c => c is TelegramBotChatAuthorizedAttribute);

                var methods = controllerType.GetMethods();

                var commandMethods = methods.Where(m => m.GetCustomAttributes(false).Any(a => a is TelegramBotCommandAttribute));
                
                foreach(var method in commandMethods)
                {
                    var methodCustomAttribute = method
                        .GetCustomAttributes(false)
                        .Where(a => a is TelegramBotCommandAttribute)
                        .Cast<TelegramBotCommandAttribute>();

                    if (!methodCustomAttribute.Any(m => m.Validate(update, _botId)))
                        continue;

                    var parameters = method.GetParameters();

                    if(parameters.Length > 0 && parameters.Any(p => p.GetCustomAttributes(false).Any(c => c is FromUpdateAttribute)) == false)
                    {
                        continue;
                    }

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

                    var parameterValues = new object[parameters.Count()];
                    var i = 0;
                    foreach(var parameter in parameters)
                    {
                        var fromUpdateAttribute = parameter
                            .GetCustomAttributes(false)
                            .FirstOrDefault(c => c is FromUpdateAttribute)
                            as FromUpdateAttribute;

                        object parameterValue = update;

                        foreach(var name in fromUpdateAttribute.Path)
                        {
                            var property = parameterValue.GetType().GetProperty(name);
                            parameterValue = property.GetValue(parameterValue);
                        }

                        if (parameter.ParameterType != typeof(string) && parameterValue is null ||
                            parameterValue.GetType() != parameter.ParameterType)
                            throw new Exception();

                        parameterValues[i++] = parameterValue;
                    }

                    controller.TelegramUpdate = update;
                    var invokeResult = method.Invoke(controller, parameterValues.ToArray());

                    if (invokeResult is not null)
                    {
                        if (method.ReturnType == typeof(Task))
                        {
                            await (Task)invokeResult;
                        }
                    }

                    return true;
                }
            }

            return false;
        }
    }
}