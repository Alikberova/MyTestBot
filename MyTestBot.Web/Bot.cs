using Microsoft.Extensions.Configuration;
using MyTestBot.BoredApi;
using MyTestBot.Commands;
using MyTestBot.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Web
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

            var boredService = new BoredApiService(botConfig);
            var keyboardService = new KeyboardService();

            commandsList = new List<Command>
            {
                new StartCommand(keyboardService),
                new RandomCommand(boredService, keyboardService),
                new FilterCommand(keyboardService),
                new AccessibilityCommand(keyboardService),
                //new KeyCommand(keyboardService),
                //new ParticipantsCommand(keyboardService),
                //new PriceCommand(keyboardService),
                new TypeCommand(keyboardService)
            };

            botClient = new TelegramBotClient(botConfig.Token);
            await botClient.SetWebhookAsync(botConfig.Ngrok + "/bot");

            return botClient;
        }
    }
}
