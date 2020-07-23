using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace IUB.Commands
{
    public class RandomCommand : Command
    {
        public override string Name => "random";

        public override List<object> InnerCommands => null;

        public override string Message => null;

        private readonly CommandService _commandService;

        public RandomCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(Update update, TelegramBotClient client)
        {
            await _commandService.Execute<T, RandomCommand>(update, client);
        }
    }
}
