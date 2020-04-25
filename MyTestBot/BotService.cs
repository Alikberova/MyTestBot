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
        //todo check if needed
        private readonly BotConfig _botConfig;
        private readonly TelegramBotClient _botClient;

        public BotService(BotConfig botConfig)
        {
            _botConfig = botConfig;
            _botClient = new TelegramBotClient(_botConfig.Token);
        }

        public async Task OnMessage(TelegramUpdate telegramUpdate) //remindy
        {
            Telegram.Bot.Types.Message msg = await _botClient.SendTextMessageAsync(telegramUpdate.Message.Chat.Id, $"Привет. Меня зовут Коко, я буду твоим продвинутым будильником." +
                $"Как к тебе обращаться?");
            //await _botClient.SendTextMessageAsync(msg.Chat.Id, $"Очень приятно, {msg.Text}! \n{msg.Text}о чем тебе напомнить?"); //name of user is there
            _botClient.OnMessage += _botClient_OnMessage;
        }

        private async void _botClient_OnMessage(object sender, MessageEventArgs e)
        {
            await _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Got " + e.Message);
        }
    }
}
