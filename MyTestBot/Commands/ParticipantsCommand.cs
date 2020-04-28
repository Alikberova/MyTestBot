using MyTestBot.Keyboard;
using MyTestBot.TelegramModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MyTestBot.Commands
{
    public class ParticipantsCommand : Command
    {
        public override string Name => throw new NotImplementedException();
        private readonly KeyboardService _keyboardService;

        public ParticipantsCommand(KeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public override bool Contains(TelegramMessage message)
        {
            throw new NotImplementedException();
        }

        public override Task Execute(TelegramMessage message, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}
