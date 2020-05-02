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
    public class StartCommand : Command
    {
        //todo add translator
        public override string Name => @"/start";

        public override List<string> InnerNames => null;

        private readonly KeyboardService _keyboardService;

        public StartCommand(KeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public override async Task Execute(TelegramMessage message, TelegramBotClient botClient, bool isInner)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, "Hallo! What do you want to do?" +
                "\nFilter from the activities or try random one",
                parseMode: ParseMode.Markdown, 
                false, false, 0, 
                _keyboardService.RandomOrFilterKeyboard());
        }

        public override async Task Execute(TelegramCallbackQuery callbackQuery, TelegramBotClient client, bool isInnerCommand)
        {
            
        }
    }
}