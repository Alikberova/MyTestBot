using IUB.Commands.Enums;
using IUB.Keyboard;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace IUB.Commands
{
    public class FilterCommand : Command
    {
        public override string Name => "filter";

        public override List<string> InnerNames => null; //need to be null otherwise keyboard will try shows everytime

        public override string Message => "Select which filter:";

        private readonly KeyboardService _keyboardService;

        public FilterCommand(KeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public override async Task Execute<T>(Update update, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(update.Message.Chat.Id, Message,
                parseMode: ParseMode.Markdown,
                false, false, 0,
                _keyboardService.GetKeyboard(new List<string>() { nameof(FilterEnum.Type),
                nameof(FilterEnum.Participants), nameof(FilterEnum.Price), nameof(FilterEnum.Accessibility)}));
        }
    }
}
