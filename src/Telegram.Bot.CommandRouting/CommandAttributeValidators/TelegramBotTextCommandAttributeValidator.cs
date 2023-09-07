using Telegram.Bot.CommandRouting;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.CommanndRouting
{
    internal class TelegramBotTextCommandAttributeValidator : ITelegramBotCommandAttributeValidator
    {
        private string[] commandNames;

        public TelegramBotTextCommandAttributeValidator(string[] commandNames)
        {
            this.commandNames = commandNames;
        }

        public bool Validate(Update update, long botId)
        {
            if (update is { Type: UpdateType.Message, Message.Type: MessageType.Text })
            {
                var text = update.Message.Text?.Split('@').FirstOrDefault()?.ToLower();
                return commandNames.Any(commandName => text == commandName || text == commandName);
            }

            return false;
        }
    }
}