using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTestBot.Commands
{
    public class StartCommand : Command
    {
        public override string Name => @"/start";

        //todo because of this list randomCommand works even if commented out @my_test_321_bot
        public override List<string> InnerNames => null;

        public override string Message => "Send \"filter\" to pick avocation or type \"random\"";

        public override async Task Execute<T>(Update update, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(update.Message.Chat.Id, "Hallo!");
            await client.SendTextMessageAsync(update.Message.Chat.Id, Message);
        }
    }
}