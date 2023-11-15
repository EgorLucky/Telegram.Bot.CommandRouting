using Telegram.Bot.CommandRouting.CommandAttributeValidators;

namespace Telegram.Bot.CommandRouting.Attributes
{
    public class TelegramBotMemberInviteCommandAttribute : TelegramBotCommandAttribute
    {
        public TelegramBotMemberInviteCommandAttribute()
            : base(new TelegramBotMemberInviteAttributeValidator())
        {
        }
    }
}