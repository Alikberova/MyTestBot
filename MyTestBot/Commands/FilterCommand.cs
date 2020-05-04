using MyTestBot.TelegramModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public class FilterCommand : Command
    {
        public override string Name => "filter";

        public override List<string> InnerNames => new List<string>() { "key", "type", "participants", "price", "price range",
                "accessibility", "accessibility range" };

        public override string Message => "Select which filter:";

        private readonly CommandService _commandService;

        public FilterCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(TelegramUpdate update, TelegramBotClient client, bool isInnerCommand)
        {
            await _commandService.Execute<T, FilterCommand>(update, client, isInnerCommand);
        }
    }
}
