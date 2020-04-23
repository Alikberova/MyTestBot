using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MyTestBot
{
    public class BotMessageService
    {
        private readonly BotConfig _botConfig;
        private readonly TelegramBotClient _botClient;

        public BotMessageService(BotConfig botConfig)
        {
            _botConfig = botConfig;
            _botClient = new TelegramBotClient(_botConfig.Token);
            _botClient.OnMessage += Bot_OnMessage;
        }

        public void Test()
        {
            Console.WriteLine("TEST");
        }

        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            await _botClient.SendTextMessageAsync(message.Chat.Id, "И тебе привет, добрый господин");
            //if (message?.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            //{
            //    await client.SendTextMessageAsync(message.Chat.Id, "И тебе привет, добрый господин");
            //}
        }
    }
}
