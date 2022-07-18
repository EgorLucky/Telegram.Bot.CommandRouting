using Telegram.Bot.Types;

namespace Telegram.Bot.CommandRouting
{
    public interface ITelegramBotCommandAttributeValidator
    {
        bool Validate(Update update, long botId);
    }
}