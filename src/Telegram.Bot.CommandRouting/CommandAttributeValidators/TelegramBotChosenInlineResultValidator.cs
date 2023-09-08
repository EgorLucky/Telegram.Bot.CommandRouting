using System.Text.RegularExpressions;
using Telegram.Bot.CommandRouting;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.CommanndRouting
{
    internal class TelegramBotChosenInlineResultValidator : ITelegramBotCommandAttributeValidator
    {
        private Regex[] regexes;

        public TelegramBotChosenInlineResultValidator(string[] regexPatterns)
        {
            regexes = regexPatterns.Select(p => new Regex(p)).ToArray();
        }

        public bool Validate(Update update, long botId)
        {
            if (update is { Type: UpdateType.ChosenInlineResult })
            {
                var text = update.ChosenInlineResult.Query?.ToLower();
                return regexes is not null && regexes.Any(r => r.IsMatch(text));
            }

            return false;
        }
    }
}