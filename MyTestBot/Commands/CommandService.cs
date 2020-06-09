using MyTestBot.BoredApi;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using System.Diagnostics;
using MyTestBot.Keyboard;
using Telegram.Bot.Types;
using MyTestBot.Commands.Enums;

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

        public async Task Execute<T1, T2>(Update update, TelegramBotClient client) where T2 : Command
        {
            try
            {
                //todo check how works with Commands where keyboard service only
                var type = new Type[] { typeof(CommandService) };
                var constructor = typeof(T2).GetConstructor(type);
                var res = constructor.Invoke(new object[1]);

                Command command = (T2)typeof(T2).GetConstructor(new Type[] { typeof(CommandService) }).Invoke(new object[1]);

                long chatId = 0;

                if (update.Type == UpdateType.CallbackQuery)
                {
                    chatId = update.CallbackQuery.Message.Chat.Id;
                    string inputData = update.CallbackQuery.Data;

                    if (command.InnerNames != null && (bool)command.InnerNames?.Contains(inputData))
                    {
                        await GenerateAndSendActivityToChat(command, update, client);
                    }
                    else
                    {
                        await client.SendTextMessageAsync(chatId, command.Message,
                            ParseMode.Markdown, false, false, 0,
                            _keyboardService.GetKeyboard(command.InnerNames));
                    }
                }
                else if (update.Type == UpdateType.Message) //start, random, filter
                {
                    if (command?.Name == "random")
                    {
                        await GenerateAndSendActivityToChat(command, update, client);
                    }
                    //else if (command?.Name == "filter")
                    //{
                    //    await client.SendTextMessageAsync(update.Message.Chat.Id, command.Message,
                    //        ParseMode.Markdown, false, false, 0,
                    //        _keyboardService.ReplyKeyboardMarkup(command.InnerNames));
                    //}
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        //here type of update is Message
        public async Task GenerateAndSendActivityToChat(Command command, Update update, TelegramBotClient client)
        {
            BoredActivity activity = null;
            var commandType = command.GetType();

            string text = update?.Message?.Text ?? update.CallbackQuery.Data;
            long chatId = update?.Message?.Chat?.Id ?? update.CallbackQuery.Message.Chat.Id;

            try
            {
                if (commandType == typeof(PriceCommand))
                {
                    Enum.TryParse(text, out PriceEnum priceEnum);
                    int enumValue = (int)priceEnum;
                    string valueForRequest = "0." + enumValue.ToString();
                    text = valueForRequest;
                }

                activity = GenerateActivity(commandType.Name, text);
                string content = _boredApiService.GetActivityContent(activity).Result;

                await client.SendTextMessageAsync(chatId, content, ParseMode.Markdown);
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        public BoredActivity GenerateActivity(string commandName, string inputData)
        {
            BoredActivity activity = new BoredActivity();

            var activityProps = activity.GetType().GetProperties();

            foreach (var prop in activityProps)
            {
                var propName = prop.Name;

                if (commandName.StartsWith(propName))
                {
                    activity.GetType().GetProperty(propName).SetValue(activity, inputData);
                    break; //todo break for now
                }
            }

            return activity;
        }

        //private bool TextEqualsInnerCommand<T>(T command, string text) where T : Command
        //{
        //    if (command.InnerNames == null || command.InnerNames?.Count == 0) return false;

        //    foreach (string name in command.InnerNames)
        //    {
        //        if (text == name)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
    }
}
