using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.CommandRouting;
using System.Text.Json;

namespace DomainLogic
{
    public class HandleUpdateService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<HandleUpdateService> _logger;
        private readonly TelegramBotCommandRouter _router;
        private readonly BotConfiguration _botConfiguration;

        public HandleUpdateService(
            ITelegramBotClient botClient, 
            ILogger<HandleUpdateService> logger, 
            TelegramBotCommandRouter router,
            BotConfiguration botConfiguration)
        {
            _botClient = botClient;
            _logger = logger;
            _router = router;
            _botConfiguration = botConfiguration;
        }

        public async Task EchoAsync(Update update, string token)
        {
            if(_botConfiguration.BotToken != token)
            {
                return;
            }

            var chatId = default(long);

            try
            {
                chatId = update.MyChatMember?.Chat?.Id ??
                                update?.Message?.Chat?.Id ??
                                0;

                await _router.TryRunCommand(update);
            }
#pragma warning disable CA1031
            catch (Exception exception)
#pragma warning restore CA1031
            {
                await HandleErrorAsync(exception, chatId);
            }
        }

        public async Task HandleErrorAsync(Exception exception, long chatId)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogInformation("HandleError: {ErrorMessage}", ErrorMessage);
            try
            {
                await _botClient.SendMessage(chatId, exception.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(JsonSerializer.Serialize(ex));
            }
        }
    }
}