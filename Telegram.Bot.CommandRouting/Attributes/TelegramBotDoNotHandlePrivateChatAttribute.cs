using Telegram.Bot.CommandRouting.CommandAttributeValidators;

namespace Telegram.Bot.CommandRouting.Attributes
{
    public class TelegramBotDoNotHandlePrivateChatAttribute : TelegramBotCommandAttribute
    {
        public TelegramBotDoNotHandlePrivateChatAttribute() : base(new TelegramBotDoNotHandlePrivateChatValidator()) { }
    }
}