using Telegram.Bot;
using Telegram.Bot.CommandRouting;
using Telegram.Bot.CommandRouting.Attributes;
using Telegram.Bot.Types;

namespace DomainLogic
{
    [TelegramBotDoNotHandleMessagesFromMe]
    [TelegramBotDoNotHandlePrivateChat]
    [TelegramBotChatAuthorized]
    public class GroupChatController : TelegramBotController
    {
        private readonly ITelegramBotClient _bot;

        public GroupChatController(ITelegramBotClient bot)
        {
            _bot = bot;
        }

        public async Task<bool> IsChatRegistred(long chatId)
        {
            return true;
        }

        [TelegramBotAllowAnonymousChat]
        [TelegramBotInviteCommand]
        public async Task InviteToChat(
            [FromUpdate(
                nameof(Update.MyChatMember),
                nameof(ChatMemberUpdated.Chat),
                nameof(Chat.Id))]
            long chatId,
            [FromUpdate(
                nameof(Update.MyChatMember),
                nameof(ChatMemberUpdated.Chat),
                nameof(Telegram.Bot.Types.Chat.Title))]
            string chatName)
        {
            await _bot.SendTextMessageAsync(chatId, $"bot invited to chat with name = {chatName} and id = {chatId}");
        }

        [TelegramBotTextCommand("/hello_bot", "/hello bot")]
        public async Task HelloBot(
            [FromUpdate(
                nameof(Update.Message),
                nameof(Message.Chat),
                nameof(Chat.Id))]
            long chatId,
            [FromUpdate(
                nameof(Update.Message),
                nameof(Message.From),
                nameof(Chat.Id))]
            long userId,
            [FromUpdate(
                nameof(Update.Message),
                nameof(Message.From),
                nameof(User.FirstName))]  string firstName,
            [FromUpdate(
                nameof(Update.Message),
                nameof(Message.From),
                nameof(User.LastName))]  string lastName)
        {
            await _bot.SendTextMessageAsync(chatId, $"Hello {firstName} {lastName}! Your userId = {userId}");
        }

        [TelegramBotTextCommand("/good_morning")]
        [TelegramBotRegexTextCommand(".*good.*morning.*")]
        public async Task GoodMorning(
            [FromUpdate(
                nameof(Update.Message),
                nameof(Message.Chat),
                nameof(Chat.Id))]
            long chatId,
            [FromUpdate(
                nameof(Update.Message),
                nameof(Message.Chat),
                nameof(Telegram.Bot.Types.Chat.Title))]
            string chatName)
        {
            await _bot.SendTextMessageAsync(chatId, $"Good morning, members of {chatName}!");
        }
    } 
}

