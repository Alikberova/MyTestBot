using MyTestBot.BoredApi;
using MyTestBot.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public class HeyCommand : Command
    {
        public override string Name => @"/hey";
        public readonly BoredApiService _boredApiService;

        public HeyCommand(BoredApiService boredApiService)
        {
            _boredApiService = boredApiService;
        }

        public override bool Contains(Message message)
        {
            //if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
            //    return false;

            return message.Text.Contains(Name); //todo maybe if (Text == Name) ?
        }

        public override async Task Execute(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, "You've choosed 'Hey'!", parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);

            string content = _boredApiService.GetContent().Result.Activity;
            await botClient.SendTextMessageAsync(chatId, content, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }
    }
}
