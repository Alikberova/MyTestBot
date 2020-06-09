using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTestBot.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract string Message { get; }

#nullable enable
        public abstract List<string> InnerNames { get; }

        public abstract Task Execute<T>(Update update, TelegramBotClient client);

        public bool Contains(Message message)
        {
            return message.Text.Contains(Name, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool Contains(CallbackQuery callbackQuery)
        {
            return callbackQuery.Data.Contains(Name, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
