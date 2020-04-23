using MyTestBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MyTestBot
{
    public class BotService
    {
        private readonly BotConfig _botConfig;
        private readonly TelegramBotClient _botClient;

        public BotService(BotConfig botConfig)
        {
            _botConfig = botConfig;
            _botClient = new TelegramBotClient(_botConfig.Token);
        }

        public async Task OnMessage(TelegramUpdate telegramUpdate)
        {
            await _botClient.SendTextMessageAsync(telegramUpdate.Message.Chat.Id, $"You've sent: {telegramUpdate.Message.Text}");
        }
    }
}
