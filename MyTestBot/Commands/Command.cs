using MyTestBot.TelegramModels;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public abstract class Command //todo make accept KeyboardService to avoid inject it in every class
    {
        public abstract string Name { get; }

        public abstract Task Execute(TelegramMessage message, TelegramBotClient client);

        public abstract bool Contains(TelegramMessage message);
    }
}
