using IUB.Commands.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace IUB.Commands
{
    public class PriceCommand : Command
    {
        public override string Name => nameof(FilterEnum.Price);

        public override List<string> InnerNames => new List<string>() { nameof(PriceEnum.Free), nameof(PriceEnum.Cheap),
            nameof(PriceEnum.Inexpensive), nameof(PriceEnum.Average), nameof(PriceEnum.Expensive),
            nameof(PriceEnum.VeryExpensive)
        };

        public override string Message => "Price? Describe the cost of the event";

        private readonly CommandService _commandService;

        public PriceCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(Update update, TelegramBotClient client)
        {
            await _commandService.Execute<T, PriceCommand>(update, client);
        }
    }
}
