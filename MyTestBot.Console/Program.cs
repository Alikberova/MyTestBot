using Microsoft.Extensions.Configuration;
using MyTestBot;
using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Awesome
{
    class Program
    {
        static TelegramBotClient botClient;
        static BotService _botService;

        static void Main()
        {
            //botClient = new TelegramBotClient("");

            //var me = botClient.GetMeAsync().Result;
            //Console.WriteLine(
            //  $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            //);

            //botClient.OnMessage += Bot_OnMessage;
            //botClient.StartReceiving();

            _botService = new BotService(new BotConfig());
            _botService.StartCommunicating();
            Thread.Sleep(int.MaxValue);
        }

        //static async void Bot_OnMessage(object sender, MessageEventArgs e)
        //{
        //    if (e.Message.Text != null)
        //    {
        //        Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

        //        await botClient.SendTextMessageAsync(
        //          chatId: e.Message.Chat,
        //          text: "You said:\n" + e.Message.Text
        //        );
        //    }
        //}
    }
}