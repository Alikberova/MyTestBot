using MyTestBot.TelegramModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public class ParticipantsCommand : Command
    {
        public override string Name => "participants";

        public override List<string> InnerNames => new List<string>() { "1", "2", "3", "4", "5" };

        public override string Message => "How much participants?";

        private readonly CommandService _commandService;

        public ParticipantsCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(TelegramUpdate update, TelegramBotClient client, bool isInnerCommand)
        {
            await _commandService.Execute<T, ParticipantsCommand>(update, client, isInnerCommand);
        }
    }
}
