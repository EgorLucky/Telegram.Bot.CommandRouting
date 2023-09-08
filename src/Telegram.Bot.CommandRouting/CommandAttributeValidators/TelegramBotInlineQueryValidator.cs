using System.Text.RegularExpressions;
using Telegram.Bot.CommandRouting;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.CommanndRouting
{
    internal class TelegramBotInlineQueryValidator : ITelegramBotCommandAttributeValidator
    {
        private Regex[] regexes;

        public TelegramBotInlineQueryValidator(string[] regexPatterns)
        {
            regexes = regexPatterns.Select(p => new Regex(p)).ToArray();
        }

        public bool Validate(Update update, long botId)
        {
            if (update is { Type: UpdateType.InlineQuery })
            {
                var text = update.InlineQuery.Query?.ToLower();
                return regexes is not null && regexes.Any(r => r.IsMatch(text));
            }

            return false;
        }
    }
}