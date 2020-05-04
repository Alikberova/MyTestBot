using MyTestBot.BoredApi;
using MyTestBot.TelegramModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using System.Diagnostics;
using MyTestBot.Keyboard;

namespace MyTestBot.Commands
{
    public class CommandService
    {
        private readonly BoredApiService _boredApiService;
        private readonly KeyboardService _keyboardService;

        public CommandService(BoredApiService boredApiService, KeyboardService keyboardService)
        {
            _boredApiService = boredApiService;
            _keyboardService = keyboardService;
        }

        public async Task Execute<T1, T2>(TelegramUpdate telegramUpdate, TelegramBotClient client, bool isInnerCommand) where T2 : Command
        {
            ITelegramUpdateData updateData = GetUpdateData<T1>(telegramUpdate);
            TelegramMessage message = updateData.GetMessage();
            string incomingText = updateData.GetText();

            if (message == null) message = telegramUpdate.Message;

            try
            {
                Type commandType = typeof(T2);
                var command = (T2)typeof(T2).GetConstructor(new Type[] { typeof(CommandService) }).Invoke(new object[1]);

                if (isInnerCommand && TextEqualsInnerCommand<Command>(command, incomingText))
                {
                    await AnswerOnInnerCommand(command, telegramUpdate, client);
                }
                else
                {
                    if (string.IsNullOrEmpty(command.Message)) return;

                    await client.SendTextMessageAsync(message.Chat.Id, command.Message,
                        parseMode: ParseMode.Markdown,
                        false, false, 0,
                        _keyboardService.ReplyKeyboardMarkup(command.InnerNames));
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        public async Task AnswerOnInnerCommand(Command command, TelegramUpdate telegramUpdate, TelegramBotClient client)
        {
            int chatId;
            TelegramMessage message = telegramUpdate.Message;
            TelegramCallbackQuery callbackQuery = telegramUpdate.Callback_Query;
            var commandType = command.GetType();
            BoredActivity activity = null;
            if (message != null)
            {
                chatId = message.Chat.Id;
                activity = GenerateActivity(commandType.Name, message.Text);
            }
            else
            {
                
                activity = GenerateActivity(commandType.Name, callbackQuery.Data);
                chatId = callbackQuery.Message.Chat.Id;
            }

            var content = _boredApiService.GetActivityContent(activity).Result;
            await client.SendTextMessageAsync(chatId, content,
                parseMode: ParseMode.Markdown);
        }

        public BoredActivity GenerateActivity(string commandName, string data)
        {
            BoredActivity activity = new BoredActivity();

            var activityProps = activity.GetType().GetProperties();

            foreach (var prop in activityProps)
            {
                var propName = prop.Name;

                if (commandName.StartsWith(propName))
                {
                    activity.GetType().GetProperty(propName).SetValue(activity, data);
                    break; //todo break for now
                }
            }

            return activity;
        }

        private bool TextEqualsInnerCommand<T>(T command, string text) where T : Command
        {
            if (command.InnerNames == null || command.InnerNames?.Count == 0) return false;

            foreach (string name in command.InnerNames)
            {
                if (text == name)
                {
                    return true;
                }
            }

            return false;
        }

        public ITelegramUpdateData GetUpdateData<T>(TelegramUpdate telegramUpdate)
        {
            if (typeof(T) == typeof(TelegramMessage))
            {
                return telegramUpdate.Message;
            }

            return telegramUpdate.Callback_Query;
        }
    }
}
