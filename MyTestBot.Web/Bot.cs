﻿using Microsoft.Extensions.Configuration;
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

            var boredService = new BoredApiService();
            var keyboardService = new KeyboardService();
            var commandService = new CommandService(boredService, keyboardService);

            commandsList = new List<Command>
            {
                new StartCommand(keyboardService),
                new FilterCommand(commandService, keyboardService),
                new RandomCommand(commandService),
                new AccessibilityCommand(commandService),
                //new KeyCommand(commandService),
                new ParticipantsCommand(commandService),
                new PriceCommand(commandService),
                new TypeCommand(commandService)
            };

            botClient = new TelegramBotClient(botConfig.Token);
            await botClient.SetWebhookAsync(BotConstants.Ngrok + "/bot");

            return botClient;
        }
    }
}
