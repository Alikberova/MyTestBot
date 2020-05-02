using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyTestBot.BoredApi
{
    public class BoredApiService
    {
        public async Task<Bored> GetContent<T>(Dictionary<string, T> keyValuePairs = null)
        {
            Bored bored = null;
            try
            {
                HttpClient httpClient = new HttpClient();

                var json = JsonConvert.SerializeObject(keyValuePairs, Formatting.Indented);
                var httpContent = new StringContent(json);
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var response = httpClient.PostAsync(BotConstants.Ngrok + "/bored", httpContent).Result;
                if (!response.IsSuccessStatusCode) Debugger.Break();
                var content = await response.Content.ReadAsStringAsync();
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
