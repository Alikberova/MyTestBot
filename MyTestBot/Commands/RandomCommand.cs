using MyTestBot.TelegramModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public class RandomCommand : Command
    {
        //todo client.SendText..() calling 2 times and text sends  2times

        public override string Name => "random";

        public override List<string> InnerNames => new List<string>() { "random", "filter" };

        public override string Message => null;

        private readonly CommandService _commandService;

        public RandomCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(TelegramUpdate update, TelegramBotClient client, bool isInnerCommand)
        {
            await _commandService.Execute<T, RandomCommand>(update, client, isInnerCommand);
        }
    }
}
