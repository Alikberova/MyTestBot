using MyTestBot.TelegramModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public abstract class Command//todo (?) make accept KeyboardService to avoid inject it in every class
    {
        public abstract string Name { get; }

        #nullable enable
        public abstract List<string> InnerNames { get; }

        public abstract Task Execute(TelegramMessage message, TelegramBotClient client, bool isInnerCommand);

        public abstract Task Execute(TelegramCallbackQuery callbackQuery, TelegramBotClient client, bool isInnerCommand);

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
