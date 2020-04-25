using MyTestBot.Models;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot
{
    public class StartCommand : Command
    {
        public override string Name => @"/start";

        public override bool Contains(Message message)
        {
            //if (message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
            //    return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            //todo change to menu items
            await botClient.SendTextMessageAsync(chatId, "Hallo I'm MagiCore. I'll help you survive in coronavirus." +
                "\nOr at least not die of monotony \nPlease choose from the available commands or select 'Random' one",
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }
    }
}