using MyTestBot.BoredApi;
using MyTestBot.Keyboard;
using MyTestBot.TelegramModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MyTestBot.Commands
{
    public class RandomCommand : Command
    {
        public override string Name => @"random";
        private readonly BoredApiService _boredApiService;
        private readonly KeyboardService _keyboardService;

        public RandomCommand(BoredApiService boredApiService, KeyboardService keyboardService)
        {
            _boredApiService = boredApiService;
            _keyboardService = keyboardService;
        }

        public override bool Contains(TelegramMessage message)
        {
            return message.Text.Contains(Name);
        }

        public override async Task Execute(TelegramMessage message, TelegramBotClient botClient)
        {
            string content = _boredApiService.GetContent().Result.Activity;
            try
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, content, parseMode: ParseMode.Markdown,
                false, false, 0, 
                _keyboardService.ReplyKeyboardMarkup(new List<string>() { "random", "change filters" }));
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }
    }
}
