using MyTestBot.TelegramModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract string Message { get; }

#nullable enable
        public abstract List<string> InnerNames { get; }

        public abstract Task Execute<T>(TelegramUpdate update, TelegramBotClient client, bool isInnerCommand);

        public bool Contains(TelegramMessage message)
        {
            return message.Text.Contains(Name);
        }

        public bool Contains(TelegramCallbackQuery callbackQuery)
        {
            return callbackQuery.Data.Contains(Name);
        }
    }
}
