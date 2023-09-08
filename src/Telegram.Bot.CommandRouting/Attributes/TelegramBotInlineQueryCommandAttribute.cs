using Telegram.Bot.CommanndRouting;

namespace Telegram.Bot.CommandRouting.Attributes
{
    public class TelegramBotInlineQueryCommandAttribute : TelegramBotCommandAttribute
    {
        public TelegramBotInlineQueryCommandAttribute(params string[] commandNames)
            : base(new TelegramBotInlineQueryValidator(commandNames))
        {
        }

    }
}