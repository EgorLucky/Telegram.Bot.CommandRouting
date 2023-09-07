using Telegram.Bot.CommandRouting;
using Telegram.Bot.Types;

namespace DomainLogic
{
    public class AuthorizationChatHandler : IAuthorizationChatHandler
    {
        private readonly GroupChatController _controller;

        public AuthorizationChatHandler(IEnumerable<TelegramBotController> controllers)
        {
            _controller = controllers.FirstOrDefault(c => c is GroupChatController) as GroupChatController;
        }
        public async Task<bool> CheckAuthorization(Update update)
        {
            var chatId = update.MyChatMember?.Chat?.Id ??
                                update?.Message?.Chat?.Id ??
                                0;
            return _controller is not null && await _controller.IsChatRegistred(chatId);
        }
    }
}