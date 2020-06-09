using MyTestBot.Commands.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTestBot.Commands
{
    public class AccessibilityCommand : Command
    {
        public override string Name => nameof(FilterEnum.Accessibility);

        public override List<string> InnerNames => new List<string>() { "0", "0.1", "0.2", "0.3", "0.4", "0.5", "0.6",
            "0.7", "0.8", "0.9", "1" };

        public override string Message => "Accessibility? How possible an event is to do with zero being the most accessible";

        private readonly CommandService _commandService;

        public AccessibilityCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(Update update, TelegramBotClient client)
        {
            await _commandService.Execute<T, AccessibilityCommand>(update, client);
        }
    }
}
