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
        public override string Name => "random";

        public override List<string> InnerNames => null;

        private readonly KeyboardService _keyboardService;
        private readonly BoredApiService _boredApiService;

        public RandomCommand(KeyboardService keyboardService, BoredApiService boredApiService)
        {
            _keyboardService = keyboardService;
            _boredApiService = boredApiService;
        }

        public override async Task Execute(TelegramMessage message, TelegramBotClient client, bool isInner)
        {
            string content = _boredApiService.GetContent<string>().Result.Activity;
            try
            {
                await client.SendTextMessageAsync(message.Chat.Id, content, parseMode: ParseMode.Markdown,
                false, false, 0, 
                _keyboardService.RandomOrFilterKeyboard());
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        public override async Task Execute(TelegramCallbackQuery callbackQuery, TelegramBotClient client, bool isInnerCommand)
        {
            string content = _boredApiService.GetContent<string>().Result.Activity;
            try
            {
                await client.SendTextMessageAsync(callbackQuery.Message.Chat.Id, content, parseMode: ParseMode.Markdown,
                false, false, 0,
                _keyboardService.RandomOrFilterKeyboard());
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }
    }
}
