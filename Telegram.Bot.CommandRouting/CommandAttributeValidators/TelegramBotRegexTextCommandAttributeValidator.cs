using System.Text.RegularExpressions;
using Telegram.Bot.CommandRouting;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.CommanndRouting
{
    internal class TelegramBotRegexTextCommandAttributeValidator : ITelegramBotCommandAttributeValidator
    {
        private Regex[] regexes;

        public TelegramBotRegexTextCommandAttributeValidator(string[] regexPatterns)
        {
            regexes = regexPatterns.Select(p => new Regex(p)).ToArray();
        }

        public bool Validate(Update update, long botId)
        {
            if (update is { Type: UpdateType.Message, Message.Type: MessageType.Text })
            {
                var text = update.Message.Text?.ToLower();
                return regexes is not null && regexes.Any(r => r.IsMatch(text));
            }

            return false;
        }
    }
}