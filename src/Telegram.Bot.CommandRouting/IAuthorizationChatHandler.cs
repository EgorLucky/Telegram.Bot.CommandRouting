using Telegram.Bot.Types;

namespace Telegram.Bot.CommandRouting
{
    public interface IAuthorizationChatHandler
    {
        Task<bool> CheckAuthorization(Update update);
    }
}