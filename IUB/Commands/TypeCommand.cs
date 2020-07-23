using IUB.Commands.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace IUB.Commands
{
    public class TypeCommand : Command
    {
        public override string Name => nameof(FilterEnum.Type);

        public override List<object> InnerCommands => new List<object>() { "education", "recreational",
                    "social", "diy", "charity", "cooking", "relaxation", "music", "busywork" };

        public override string Message => "Which type of activity?";

        private readonly CommandService _commandService;

        public TypeCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(Update update, TelegramBotClient client)
        {
            await _commandService.Execute<T, TypeCommand>(update, client);
        }
    }
}
