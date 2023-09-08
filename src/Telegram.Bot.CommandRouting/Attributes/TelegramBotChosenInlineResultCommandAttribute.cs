using Telegram.Bot.CommanndRouting;

namespace Telegram.Bot.CommandRouting.Attributes
{
    public class TelegramBotChosenInlineResultCommandAttribute : TelegramBotCommandAttribute
    {
        public TelegramBotChosenInlineResultCommandAttribute(params string[] commandNames)
            : base(new TelegramBotChosenInlineResultValidator(commandNames))
        {
        }

    }
}