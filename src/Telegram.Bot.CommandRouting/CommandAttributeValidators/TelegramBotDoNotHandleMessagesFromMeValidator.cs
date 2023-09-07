using Telegram.Bot.Types;

namespace Telegram.Bot.CommandRouting.CommandAttributeValidators
{
    internal class TelegramBotDoNotHandleMessagesFromMeValidator : ITelegramBotCommandAttributeValidator
    {
        public bool Validate(Update update, long botId) => botId != update?.Message?.From?.Id;
    }
}