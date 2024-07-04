using Telegram.Bot.CommanndRouting;

namespace Telegram.Bot.CommandRouting.Attributes;

public class TelegramBotVideoCommandAttribute : TelegramBotCommandAttribute
{
    public TelegramBotVideoCommandAttribute()
        : base(new TelegramBotVideoAttributeValidator())
    {
    }
}