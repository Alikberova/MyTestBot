using MyTestBot.BoredApi;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using MyTestBot.Keyboard;
using Telegram.Bot.Types;
using MyTestBot.Commands.Enums;
using MyTestBot.Translate;
using System.Net.Http;
using Newtonsoft.Json;
using Serilog;
using System.Text;
using System.Diagnostics;

namespace MyTestBot.Commands
{
    public class CommandService
    {
        private readonly ActivityService _boredApiService;
        private readonly KeyboardService _keyboardService;
        private readonly TranslateService _translateService;
        private readonly BotConfig _botConfig;

        public CommandService(ActivityService boredApiService, KeyboardService keyboardService, 
            TranslateService translateService, BotConfig botConfig)
        {
            _boredApiService = boredApiService;
            _keyboardService = keyboardService;
            _translateService = translateService;
            _botConfig = botConfig;
        }

        public async Task Execute<T1, T2>(Update update, TelegramBotClient client) where T2 : Command
        {
            try
            {
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
                        await SendActivityToChat(command, update, client);
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
                        await SendActivityToChat(command, update, client);
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Break(); Log.Error(ex, ex.Message);
            }
        }

        //here type of update is Message
        public async Task SendActivityToChat(Command command, Update update, TelegramBotClient client)
        {
            ActivityModel activityRequest = null;
            var commandType = command.GetType();

            string text = update?.Message?.Text ?? update.CallbackQuery.Data;
            long chatId = update?.Message?.Chat?.Id ?? update.CallbackQuery.Message.Chat.Id;

            try
            {
                if (commandType == typeof(PriceCommand)) //todo check if possible delete
                {
                    Enum.TryParse(text, out PriceEnum priceEnum);
                    int enumValue = (int)priceEnum;
                    string valueForRequest = "0." + enumValue.ToString();
                    text = valueForRequest;
                }

                activityRequest = GenerateActivityRequest(commandType.Name, text);
                ActivityModel activityResult = _boredApiService.GetActivityContent(activityRequest).Result;
                activityResult = await SetValueToActivityResult(activityResult);
                await SendToDb(activityResult);
                await client.SendTextMessageAsync(chatId, activityResult.Activity + $"\n({activityResult.ActivityRu})",
                    ParseMode.Markdown);
            }
            catch (Exception ex)
            {
                Debugger.Break(); Log.Error(ex, ex.Message);
            }
        }

        public ActivityModel GenerateActivityRequest(string commandName, string userInput)
        {
            ActivityModel activity = new ActivityModel();

            var properties = activity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyName = property.Name;

                if (propertyName == "Id" || propertyName == nameof(ActivityModel.ActivityRu) || 
                    propertyName == nameof(ActivityModel.CreatedDate) || propertyName == nameof(ActivityModel.ModifiedDate))
                {
                    continue;
                }

                if (commandName.StartsWith(propertyName))
                {
                    if (commandName.Contains(nameof(ActivityModel.Accessibility), StringComparison.CurrentCultureIgnoreCase))
                    {
                        double convertedInput = double.Parse(userInput);
                        SetValueToActivityRequest(activity, propertyName, convertedInput);
                    }
                    else if (commandName.Contains(nameof(ActivityModel.Participants), StringComparison.CurrentCultureIgnoreCase))
                    {
                        int convertedInputData = int.Parse(userInput);
                        SetValueToActivityRequest(activity, propertyName, convertedInputData);
                    }
                    else if (commandName.Contains(nameof(ActivityModel.Price), StringComparison.CurrentCultureIgnoreCase))
                    {
                        userInput = userInput.Substring(2);
                        Enum.TryParse(userInput, out PriceEnum priceEnum);
                        SetValueToActivityRequest(activity, propertyName, priceEnum);
                    }
                    else
                    {
                        SetValueToActivityRequest(activity, propertyName, userInput);
                    }
                    break; //todo later think how to set few parameters
                }
            }

            return activity;
        }

        private void SetValueToActivityRequest(ActivityModel activity, string propertyName, object value)
        {
            //todo check if works
            activity.GetType().GetProperty(propertyName).SetValue(activity, value);
        }

        private async Task<ActivityModel> SetValueToActivityResult(ActivityModel activity)
        {
            string en = activity.Activity;
            string ru = await _translateService.Translate(en);

            activity.Id = Guid.NewGuid();
            activity.CreatedDate = DateTime.Now;
            activity.ActivityRu = ru;

            return activity;
        }

        private async Task SendToDb(ActivityModel activity)
        {
            HttpClient client = new HttpClient();
            string activityString = JsonConvert.SerializeObject(activity);
            HttpContent content = new StringContent(activityString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_botConfig.WebsiteUrl + "/api/activity", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
