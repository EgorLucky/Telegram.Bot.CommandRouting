using Telegram.Bot.CommanndRouting;

namespace Telegram.Bot.CommandRouting.Attributes;

public class TelegramBotDocumentCommandAttribute : TelegramBotCommandAttribute
{
    public TelegramBotDocumentCommandAttribute()
        : base(new TelegramBotDocumentAttributeValidator())
    {
    }
}