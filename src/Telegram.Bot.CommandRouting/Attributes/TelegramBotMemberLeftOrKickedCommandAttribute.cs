using Telegram.Bot.CommandRouting.CommandAttributeValidators;

namespace Telegram.Bot.CommandRouting.Attributes
{
    public class TelegramBotMemberLeftOrKickedCommandAttribute : TelegramBotCommandAttribute
    {
        public TelegramBotMemberLeftOrKickedCommandAttribute()
            : base(new TelegramBotMemberLeftOrKickedAttributeValidator())
        {
        }
    }
}