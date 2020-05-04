using MyTestBot.TelegramModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public class PriceCommand : Command
    {
        public override string Name => "price";

        public override List<string> InnerNames => new List<string>() { "0", "0.1", "0.2", "0.3", "0.4", "0.5", "0.6" };

        public override string Message => "Price? Describe the cost of the event with zero being free";

        private readonly CommandService _commandService;

        public PriceCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(TelegramUpdate update, TelegramBotClient client, bool isInnerCommand)
        {
            await _commandService.Execute<T, PriceCommand>(update, client, isInnerCommand);
        }
    }
}
