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
    public class FilterCommand : Command
    {
        public override string Name => "filter";
        private readonly KeyboardService _keyboardService;

        public FilterCommand(KeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public override bool Contains(TelegramMessage message)
        {
            return message.Text.Contains(Name);
        }

        public override async Task Execute(TelegramMessage message, TelegramBotClient client)
        {
            string text = "Please, choose comething good!";
            var keyboardButtonsNames = new List<string>() { "key", "type", "participants", "price", "price range", 
                "accessibility", "accessibility range" };
            try
            {
                await client.SendTextMessageAsync(message.Chat.Id, text, parseMode: ParseMode.Markdown,
                false, false, 0, 
                _keyboardService.ReplyKeyboardMarkup(keyboardButtonsNames));
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }
    }
}
