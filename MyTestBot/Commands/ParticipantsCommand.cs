using MyTestBot.BoredApi;
using MyTestBot.Keyboard;
using MyTestBot.TelegramModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MyTestBot.Commands
{
    public class ParticipantsCommand : Command
    {
        public override string Name => "participants";

        public override List<string> InnerNames => new List<string>() { "1", "2", "3", "4", "5" };

        private readonly KeyboardService _keyboardService;
        private readonly BoredApiService _boredApiService;

        public ParticipantsCommand(KeyboardService keyboardService, BoredApiService boredApiService)
        {
            _keyboardService = keyboardService;
            _boredApiService = boredApiService;
        }

        public override async Task Execute(TelegramMessage message, TelegramBotClient client, bool isInner)
        {
            if (TextEqualsInnerCommand(message.Text))
            {
                await AnswerOnInnerCommand(client, message, null);
            }
            else
            {
                var chatId = message.Chat.Id;
                await client.SendTextMessageAsync(chatId, "How much participants?",
                    parseMode: ParseMode.Markdown,
                    false, false, 0,
                    _keyboardService.ReplyKeyboardMarkup(InnerNames));
            }
        }

        public override async Task Execute(TelegramCallbackQuery callbackQuery, TelegramBotClient client, bool isInnerCommand)
        {
            try
            {
                if (isInnerCommand && TextEqualsInnerCommand(callbackQuery.Data))
                {
                    await AnswerOnInnerCommand(client, null, callbackQuery);
                }
                else
                {
                    await client.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "How much participants?",
                        parseMode: ParseMode.Markdown,
                        false, false, 0,
                        _keyboardService.ReplyKeyboardMarkup(InnerNames));
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        private async Task AnswerOnInnerCommand(TelegramBotClient client, TelegramMessage message = null, TelegramCallbackQuery callbackQuery = null)
        {
            int chatId;
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            if (message != null)
            {
                chatId = message.Chat.Id;
                keyValuePairs.Add(Name, message.Text);
            }
            else
            {
                chatId = callbackQuery.Message.Chat.Id;
                keyValuePairs.Add(Name, callbackQuery.Data);
            }

            var content = _boredApiService.GetContent(keyValuePairs).Result.Activity;
            await client.SendTextMessageAsync(chatId, content,
                parseMode: ParseMode.Markdown);
        }

        private bool TextEqualsInnerCommand(string text)
        {
            foreach (string name in InnerNames)
            {
                if (text == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
