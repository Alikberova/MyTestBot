using MyTestBot.TelegramModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public class AccessibilityCommand : Command //todo add tip/prompt for commands
    {
        public override string Name => "accessibility";

        public override List<string> InnerNames => new List<string>() { "0", "0.1", "0.2", "0.3", "0.4", "0.5", "0.6",
            "0.7", "0.8", "0.9", "1" };

        public override string Message => "Accessibility? How possible an event is to do with zero being the most accessible";

        private readonly CommandService _commandService;

        public AccessibilityCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(TelegramUpdate update, TelegramBotClient client, bool isInnerCommand)
        {
            await _commandService.Execute<T, AccessibilityCommand>(update, client, isInnerCommand);
        }
    }
}
