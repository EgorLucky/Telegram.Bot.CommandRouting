using Telegram.Bot.CommandRouting.CommandAttributeValidators;

namespace Telegram.Bot.CommandRouting.Attributes
{
    public class TelegramBotInviteCommandAttribute : TelegramBotCommandAttribute
    {
        public TelegramBotInviteCommandAttribute() 
            : base(new TelegramBotInviteAttributeValidator())
        {
        }
    }
}