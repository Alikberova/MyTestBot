using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyTestBot.BoredApi
{
    public class BoredApiService
    {
        //todo Get Activity, accessibility, type etc
        private readonly BotConfig _botConfig;

        public BoredApiService(BotConfig botConfig)
        {
            _botConfig = botConfig;
        }

        public async Task<Bored> GetContent()
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync(_botConfig.Ngrok + "/bored").Result;
            var content = await response.Content.ReadAsStringAsync();

            Bored bored = null;
            try
            {
                bored = JsonConvert.DeserializeObject<Bored>(content);
            }
            catch (Exception ex)
            {
                Debugger.Break();
                Console.WriteLine(ex.Message);
            }
            return bored;
        }
    }
}
