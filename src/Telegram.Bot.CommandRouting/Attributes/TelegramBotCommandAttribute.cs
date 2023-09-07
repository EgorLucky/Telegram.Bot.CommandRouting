using Telegram.Bot.Types;

namespace Telegram.Bot.CommandRouting.Attributes
{
    public class TelegramBotCommandAttribute : Attribute
    {
        private readonly ITelegramBotCommandAttributeValidator _validator;
        public TelegramBotCommandAttribute(ITelegramBotCommandAttributeValidator validator)
        {
            _validator = validator;
        }

        public bool Validate(Update update, long botId)
        {
            return _validator.Validate(update, botId);
        }
    }
}