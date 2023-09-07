using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.CommandRouting.CommandAttributeValidators
{
    internal class TelegramBotDoNotHandlePrivateChatValidator : ITelegramBotCommandAttributeValidator
    {
        public bool Validate(Update update, long botId)
        {
            var chat = update.Message?.Chat ?? update.MyChatMember?.Chat;
            if (chat.Type is not ChatType.Private)
                return true;

            return false;
        }
    }
}