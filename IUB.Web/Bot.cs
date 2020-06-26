using Microsoft.Extensions.Configuration;
using IUB.BoredApi;
using IUB.Commands;
using IUB.Keyboard;
using IUB.Translate;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace IUB.Web
{
    public class Bot
    {
        private static TelegramBotClient botClient;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (botClient != null)
            {
                return botClient;
            }

            BotConfig botConfig = Startup.StaticConfig.GetSection("BotConfig").Get<BotConfig>();

            var boredService = new ActivityService();
            var keyboardService = new KeyboardService();
            var translateService = new TranslateService(botConfig);
            var commandService = new CommandService(boredService, keyboardService, translateService, botConfig);

            commandsList = new List<Command>
            {
                new StartCommand(),
                new FilterCommand(keyboardService),
                new RandomCommand(commandService),
                new AccessibilityCommand(commandService),
                new ParticipantsCommand(commandService),
                new PriceCommand(commandService),
                new TypeCommand(commandService)
            };

            botClient = new TelegramBotClient(botConfig.TelegramConfig.Token);
            await botClient.SetWebhookAsync(botConfig.WebsiteUrl + "/bot");

            return botClient;
        }
    }
}
