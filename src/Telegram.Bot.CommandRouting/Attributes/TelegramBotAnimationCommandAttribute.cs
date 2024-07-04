using Telegram.Bot.CommanndRouting;

namespace Telegram.Bot.CommandRouting.Attributes;

public class TelegramBotAnimationCommandAttribute : TelegramBotCommandAttribute
{
    public TelegramBotAnimationCommandAttribute()
        : base(new TelegramBotAnimationAttributeValidator())
    {
    }
}