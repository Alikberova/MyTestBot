using Microsoft.Extensions.Configuration;
using MyTestBot.BoredApi;
using MyTestBot.Commands;
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
        private static BotConfig _appSettings;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public Bot(BotConfig appSettings)
        {
            _appSettings = appSettings;
        }

        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (botClient != null)
            {
                return botClient;
            }
            commandsList = new List<Command>();
            commandsList.Add(new StartCommand());
            commandsList.Add(new HeyCommand(new BoredApiService()));

            botClient = new TelegramBotClient("");
            string hook = "https://900bf880.ngrok.io/bot";
            await botClient.SetWebhookAsync(hook);
            return botClient;
        }
    }

    //public class Test
    //{
    //    private AppSettings _botConfig;

    //    public Test(AppSettings botConfig)
    //    {
    //        _botConfig = botConfig;
    //    }

    //    public string Tt()
    //    {
    //        var res = _botConfig.BotConfig.Ngrok;
    //        return res;
    //    }
    //}
}
