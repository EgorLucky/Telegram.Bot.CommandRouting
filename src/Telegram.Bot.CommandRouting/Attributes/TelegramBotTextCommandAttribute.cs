using Telegram.Bot.CommanndRouting;

namespace Telegram.Bot.CommandRouting.Attributes
{
    public class TelegramBotTextCommandAttribute : TelegramBotCommandAttribute
    {
        public TelegramBotTextCommandAttribute(params string[] commandNames)
            : base(new TelegramBotTextValidator(commandNames))
        {
        }

    }
}