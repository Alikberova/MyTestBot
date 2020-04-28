using MyTestBot.Keyboard;
using MyTestBot.TelegramModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MyTestBot.Commands
{
    public class StartCommand : Command
    {
        //todo add translator
        public override string Name => @"/start";
        private readonly KeyboardService _keyboardService;

        public StartCommand(KeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public override bool Contains(TelegramMessage message)
        {
            //todo
            //if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
            //    return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(TelegramMessage message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId, "Hallo I'm MagiCore. I'll help you survive in coronavirus." +
                "\nOr at least not die of monotony \nPlease filter from the available activities or try random one",
                parseMode: ParseMode.Markdown, 
                false, false, 0, 
                _keyboardService.ReplyKeyboardMarkup(new List<string>() { "random", "filter" }));
        }
    }
}