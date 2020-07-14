using IUB.BoredApi;
using IUB.Commands;
using IUB.Commands.Enums;
using IUB.Keyboard;
using IUB.Translating;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IUB.Tests
{
    public class UnitTest1
    {
        private readonly CommandService _commandService;
        private readonly BotConfig _botConfig;
        private readonly ActivityService _activityService;

        //todo take last from db
        //todo check generating activity request
        public UnitTest1()
        {
            _botConfig = GetConfig();
            _activityService = new ActivityService();
            _commandService = new CommandService(_activityService, new KeyboardService(), 
                new TranslatingService(_botConfig), _botConfig);
        }

        [Fact]
        public async void CheckActivity()
        {
            var commands = GetCommands();
            Command accessibility = commands.FirstOrDefault();
            Command price = commands.ElementAt(1);
            Command participants = commands.LastOrDefault();

            await CheckAccessibilityCommand(accessibility);
            CheckPriceInActivityRequest(price);
            CheckParticipantsInActivityRequest(participants);
        }

        //[Fact]
        public void CheckActivitiesInDb()
        {
            ActivityModel expected = GetExpectedActivityRequest(0.5, PriceEnum.Unspecified, null);
            ActivityModel expected2 = GetExpectedActivityRequest(null, PriceEnum.Inexpensive, null);
            ActivityModel expected3 = GetExpectedActivityRequest(null, PriceEnum.Unspecified, 3);
        }

        private async Task CheckAccessibilityCommand(Command command)
        {
            for (int i = 1; i < command.InnerNames.Count; i++)
            {
                var userInput = double.Parse(command.InnerNames[i]);
                var expectedRequest = GetExpectedActivityRequest(userInput, null, null);
                var actualRequest = _commandService.GenerateActivityRequest(command.Name, userInput.ToString());

                AssertActivity(expectedRequest, actualRequest);
                await GetActivitiesFromDb();
                await _commandService.SendToDb(actualRequest);
            }
        }

        private void CheckPriceInActivityRequest(Command command)
        {
            for (int i = 1; i < command.InnerNames.Count; i++)
            {
                var expected = GetExpectedActivityRequest(null, (PriceEnum)i, null);
                var actual = _commandService.GenerateActivityRequest(command.Name, i.ToString());
                AssertActivity(expected, actual);
            }
        }

        private void CheckParticipantsInActivityRequest(Command command)
        {
            for (int i = 1; i < command.InnerNames.Count; i++)
            {
                var userInput = int.Parse(command.InnerNames[i]);
                var expected = GetExpectedActivityRequest(null, null, userInput);
                var actual = _commandService.GenerateActivityRequest(command.Name, userInput.ToString());
                AssertActivity(expected, actual);
            }
        }

        private async Task<List<ActivityModel>> GetActivitiesFromDb()
        {
            //todo inject context
            var client = new HttpClient();
            var response = await client.GetAsync(_botConfig.WebsiteUrl + "/api/Activity");
            string activitiesString = await response.Content.ReadAsStringAsync();

            Checking ch = JsonConvert.DeserializeObject<Checking>(activitiesString);
            return ch.Activities.ToList();
        }

        private void AssertActivity(ActivityModel expected, ActivityModel actual)
        {
            Assert.Equal(expected.Accessibility, actual.Accessibility);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Participants, actual.Participants);
        }

        private ActivityModel GetExpectedActivityRequest(double? accessibility, PriceEnum? price, int? participants)
        {
            var activity = new ActivityModel();
            activity.Accessibility = accessibility;
            activity.Price = price != null ? (PriceEnum)price : activity.Price;
            activity.Participants = participants;

            return activity;
        }

        private List<Command> GetCommands()
        {
            return new List<Command>()
            {
                new AccessibilityCommand(_commandService),
                new PriceCommand(_commandService),
                new ParticipantsCommand(_commandService)
            };
        }

        private BotConfig GetConfig()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            return configuration.GetSection("BotConfig").Get<BotConfig>();
        }
    }
}
