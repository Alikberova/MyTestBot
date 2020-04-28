using MyTestBot.Keyboard;
using MyTestBot.TelegramModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MyTestBot.Commands
{
    public class AccessibilityCommand : Command //todo add tip/prompt for commands
    {
        public override string Name => "accessibility";
        private readonly KeyboardService _keyboardService;

        public AccessibilityCommand(KeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public override bool Contains(TelegramMessage message)
        {
            return message.Text.Contains(Name);
        }

        public override async Task Execute(TelegramMessage message, TelegramBotClient client)
        {
            //todo maybe add for ..range here also and the same for price range
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, "",
                parseMode: ParseMode.Markdown,
                false, false, 0,
                _keyboardService.ReplyKeyboardMarkup(new List<string>() { "" }));
        }
    }
}
