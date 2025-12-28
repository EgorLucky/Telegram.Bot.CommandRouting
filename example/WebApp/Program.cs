using DomainLogic;
using Telegram.Bot;
using Telegram.Bot.CommandRouting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var botConfig = new BotConfiguration
{
    BotToken = builder.Configuration.GetValue<string>("botToken"),
    HostAddress = builder.Configuration.GetValue<string>("webAppHost"),
};

builder.Services.AddSingleton(botConfig);
builder.Services.AddHostedService<ConfigureWebhook>();
builder.Services.AddHttpClient("tgwebhook")
    .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(botConfig.BotToken, httpClient));
builder.Services
    .AddScoped<HandleUpdateService>()
    .AddScoped<TelegramBotCommandRouter>()
    .AddScoped<TelegramBotController, GroupChatController>()
    .AddScoped<TelegramBotController, AllChatTypesController>()
    .AddScoped<IAuthorizationChatHandler, AuthorizationChatHandler>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
