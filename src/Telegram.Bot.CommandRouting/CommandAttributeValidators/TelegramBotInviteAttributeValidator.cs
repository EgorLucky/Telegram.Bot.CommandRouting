using Telegram.Bot.CommandRouting;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.CommandRouting.CommandAttributeValidators
{
    internal class TelegramBotInviteAttributeValidator : ITelegramBotCommandAttributeValidator
    {
        public bool Validate(Update update, long botId)
        {
            if (update.Type == UpdateType.MyChatMember)
            {
                var newChatMember = update.MyChatMember.NewChatMember;

                if (newChatMember.Status == ChatMemberStatus.Member && newChatMember.User.Id == botId)
                    return true;
            }

            return false;
        }
    }
}