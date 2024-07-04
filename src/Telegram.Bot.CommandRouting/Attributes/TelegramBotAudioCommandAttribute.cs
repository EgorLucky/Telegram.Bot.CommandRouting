using Telegram.Bot.CommanndRouting;

namespace Telegram.Bot.CommandRouting.Attributes;

public class TelegramBotAudioCommandAttribute : TelegramBotCommandAttribute
{
    public TelegramBotAudioCommandAttribute()
        : base(new TelegramBotAudioAttributeValidator())
    {
    }
}