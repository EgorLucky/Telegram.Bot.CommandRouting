using Telegram.Bot.CommandRouting.CommandAttributeValidators;
using Telegram.Bot.CommanndRouting;

namespace Telegram.Bot.CommandRouting.Attributes;

public class TelegramBotPhotoCommandAttribute : TelegramBotCommandAttribute
{
    public TelegramBotPhotoCommandAttribute()
        : base(new TelegramBotPhotoAttributeValidator())
    {
    }
}