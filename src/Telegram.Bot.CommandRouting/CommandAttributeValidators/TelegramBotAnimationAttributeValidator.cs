using Telegram.Bot.CommandRouting;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.CommanndRouting;

internal class TelegramBotAnimationAttributeValidator : ITelegramBotCommandAttributeValidator
{
    public TelegramBotAnimationAttributeValidator()
    {
    }

    public bool Validate(Update update, long botId)
    {
        return update is { Type: UpdateType.Message, Message.Type: MessageType.Animation };
    }
}