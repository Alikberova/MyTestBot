using MyTestBot.TelegramModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public class StartCommand : Command
    {
        //todo add translator
        public override string Name => @"/start";

        //todo because of this list randomCommand works even if commented out
        public override List<string> InnerNames => new List<string>() { "random", "filter" };

        public override string Message => "Hallo! What do you want to do?\nFilter from the activities or try random one";

        private readonly CommandService _commandService;

        public StartCommand(CommandService commandService)
        {
            _commandService = commandService;
        }

        public override async Task Execute<T>(TelegramUpdate update, TelegramBotClient client, bool isInnerCommand)
        {
            await _commandService.Execute<T, StartCommand>(update, client, isInnerCommand);
        }
    }
}