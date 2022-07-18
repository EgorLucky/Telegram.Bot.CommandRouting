using Telegram.Bot.CommandRouting.CommandAttributeValidators;

namespace Telegram.Bot.CommandRouting.Attributes
{
    public class TelegramBotDoNotHandleMessagesFromMe : TelegramBotCommandAttribute
    {
        public TelegramBotDoNotHandleMessagesFromMe (): base(new TelegramBotDoNotHandleMessagesFromMeValidator()) {}
    }
}