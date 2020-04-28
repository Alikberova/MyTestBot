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
    public class TypeCommand : Command
    {
        public override string Name => "type";
        private readonly KeyboardService _keyboardService;

        public TypeCommand(KeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public override bool Contains(TelegramMessage message)
        {
            return message.Text.Contains(Name);
        }

        public override async Task Execute(TelegramMessage message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, "Select what do you will select!",
                parseMode: ParseMode.Markdown,
                false, false, 0,
                _keyboardService.ReplyKeyboardMarkup(new List<string>() { "education", "recreational", 
                    "social", "diy", "charity", "cooking", "relaxation", "music", "busywork" }));
        }
    }
}
