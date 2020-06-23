using MyTestBot.Commands.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTestBot.Commands
{
    public class ParticipantsCommand : Command
    {
        //todo participants works as accessibility with "How much participants? - only when 1"
        public override string Name => nameof(FilterEnum.Participants);

        public override List<string> InnerNames => new List<string>() { "1", "2", "3", "4", "5" };

        public override string Message => "How much participants?";

        private readonly CommandService _commandService;

        public ParticipantsCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(Update update, TelegramBotClient client)
        {
            await _commandService.Execute<T, ParticipantsCommand>(update, client);
        }
    }
}
