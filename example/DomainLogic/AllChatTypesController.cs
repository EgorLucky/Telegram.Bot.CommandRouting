using Telegram.Bot;
using Telegram.Bot.CommandRouting;
using Telegram.Bot.CommandRouting.Attributes;
using Telegram.Bot.Types;

namespace DomainLogic
{
    [TelegramBotDoNotHandleMessagesFromMe]
    public class AllChatTypesController : TelegramBotController
    {
        private readonly ITelegramBotClient _bot;

        public AllChatTypesController(ITelegramBotClient bot)
        {
            _bot = bot;
        }

        [TelegramBotTextCommand("/randomnumber")]
        public async Task GetRandomNumber(
            [FromUpdate(
                nameof(Update.Message),
                nameof(Message.Chat),
                nameof(Chat.Id))]
            long userId)
        {
            await _bot.SendMessage(userId, $"your random number is {Random.Shared.Next()}");
        } 
    }
}
