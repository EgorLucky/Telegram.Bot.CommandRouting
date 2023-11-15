using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.CommandRouting.CommandAttributeValidators
{
    internal class TelegramBotMemberInviteAttributeValidator : ITelegramBotCommandAttributeValidator
    {
        public bool Validate(Update update, long botId)
        {
            if (update.Type == UpdateType.Message && update.Message.NewChatMembers?.Length > 0)
            {
                return true;
            }

            return false;
        }
    }
}