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

        public override List<string> InnerNames => new List<string>() { "key", "type", "participants", "price", "price range",
                "accessibility", "accessibility range" };

        private readonly KeyboardService _keyboardService;

        public FilterCommand(KeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public override async Task Execute(TelegramMessage message, TelegramBotClient client, bool isInner)
        {
            string text = "Select which filter: ";
            try
            {
                await client.SendTextMessageAsync(message.Chat.Id, text, parseMode: ParseMode.Markdown,
                false, false, 0, 
                _keyboardService.FiltersOfActivityKeyboard());
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        public override async Task Execute(TelegramCallbackQuery callbackQuery, TelegramBotClient client, bool isInnerCommand)
        {
            string text = "Select which filter: ";
            try
            {
                await client.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text, parseMode: ParseMode.Markdown,
                false, false, 0,
                _keyboardService.FiltersOfActivityKeyboard());
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }
    }
}
