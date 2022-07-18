using Telegram.Bot.CommanndRouting;

namespace Telegram.Bot.CommandRouting.Attributes
{
    public class TelegramBotRegexTextCommandAttribute : TelegramBotCommandAttribute
    {
        public TelegramBotRegexTextCommandAttribute(params string[] regexPatterns)
            : base(new TelegramBotRegexTextCommandAttributeValidator(regexPatterns))
        {
        }
    }
}