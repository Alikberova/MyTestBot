using MyTestBot.TelegramModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public class TypeCommand : Command
    {
        public override string Name => "type";

        public override List<string> InnerNames => new List<string>() { "education", "recreational",
                    "social", "diy", "charity", "cooking", "relaxation", "music", "busywork" };

        public override string Message => "Which type of activity?";

        private readonly CommandService _commandService;

        public TypeCommand(CommandService commandService)
        {
            _commandService = commandService;
        }
        //todo during press "type" - 1.filters, 2.types appers in telegram

        public override async Task Execute<T>(TelegramUpdate update, TelegramBotClient client, bool isInnerCommand)
        {
            await _commandService.Execute<T, TypeCommand>(update, client, isInnerCommand);
        }
    }
}
