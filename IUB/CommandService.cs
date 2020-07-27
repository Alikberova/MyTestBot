using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using IUB.Keyboard;
using Telegram.Bot.Types;
using IUB.Translating;
using IUB.Db;
using System.Collections.Generic;
using IUB.Commands;

namespace IUB
{
    public class CommandService
    {
        private readonly KeyboardService _keyboardService;
        private readonly TranslatingService _translatingService;
        private readonly Repository _repository;

        public CommandService(KeyboardService keyboardService, 
            TranslatingService translatingService, Repository repository)
        {
            _keyboardService = keyboardService;
            _translatingService = translatingService;
            _repository = repository;
        }

        public async Task Execute<T1, T2>(Update update, TelegramBotClient client) where T2 : Command
        {
            Command command = (T2)typeof(T2)
                .GetConstructor(new Type[] { typeof(CommandService) })
                .Invoke(new object[1]);

            string input = GetTextInput(update);
            var innerCommands = new List<string>();
            command.InnerCommands?.ForEach(c => innerCommands.Add(c.ToString()));

            if (command.Name == input)
            {
                await client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, command.Message,
                    ParseMode.Markdown, false, false, 0,
                    _keyboardService.GetKeyboard(innerCommands));
            }
            else
            {
                await SendActivityMessage(command, update, client);
            }
        }

        public List<string> ToStringInnerCommands(List<object> innerCommands)
        {
            var innerCommandsStrings = new List<string>();
            innerCommands?.ForEach(c => innerCommandsStrings.Add(c.ToString()));
            return innerCommandsStrings;
        }

        private async Task SendActivityMessage(Command command, Update update, TelegramBotClient client)
        {
            string text = GetTextInput(update);
            long chatId = update?.Message?.Chat?.Id ?? update.CallbackQuery.Message.Chat.Id;
            Activity.Activity activity = null;

            if (command?.Name == "random")
            {
                activity = await _repository.GetRandom();
            }
            else
            {
                activity = await _repository.Get(command.Name, text);
            }

            await client.SendTextMessageAsync(chatId, activity.TextEn,
                    ParseMode.Markdown);
        }

        private string GetTextInput(Update update)
        {
            return update?.Message?.Text ?? update.CallbackQuery.Data;
        }
    }
}
