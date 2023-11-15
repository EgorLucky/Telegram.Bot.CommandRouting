using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.CommandRouting.CommandAttributeValidators
{
    internal class TelegramBotMemberLeftOrKickedAttributeValidator : ITelegramBotCommandAttributeValidator
    {
        public bool Validate(Update update, long botId)
        {
            if (update.Type == UpdateType.Message && update.Message.LeftChatMember is not null)
            {
                return true;
            }

            return false;
        }
    }
}