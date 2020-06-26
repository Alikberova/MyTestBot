using Newtonsoft.Json;
using Serilog;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IUB.Translate
{
    public class TranslateService
    {
        private readonly BotConfig _config;

        public TranslateService(BotConfig config)
        {
            _config = config;
        }

        public async Task<string> Translate(string input)
        {
            object[] body = new object[] { new { Text = input } };
            var requestBody = JsonConvert.SerializeObject(body);

            using HttpClient client = new HttpClient();
            using HttpRequestMessage request = new HttpRequestMessage();

            string route = "/translate?api-version=3.0&to=ru";
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(_config.TranslateConfig.EndPoint + route);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", _config.TranslateConfig.SubscriptionKey);
            request.Headers.Add("Ocp-Apim-Subscription-Region", _config.TranslateConfig.Region);

            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

            string result = await response.Content.ReadAsStringAsync();
            Translate[] translate = null;
            try
            {
                translate = JsonConvert.DeserializeObject<Translate[]>(result);
            }
            catch (Exception ex)
            {
                Debugger.Break(); Log.Error(ex, ex.Message);
            }
            return translate[0]?.Translations[0].Text;
        }
    }
}
