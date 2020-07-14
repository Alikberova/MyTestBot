using Newtonsoft.Json;
using Serilog;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IUB.Translating
{
    public class TranslatingService
    {
        private const string endOfRoute = "?api-version=3.0";

        private readonly BotConfig _config;

        public TranslatingService(BotConfig config)
        {
            _config = config;
        }

        public async Task<string> Translate(string input)
        {
            string route = RouteConstants.Translate + endOfRoute + "&to=ru";
            Translate[] translate = await SendRequestToTranslateApi(route, input);

            return translate[0]?.Translations[0].Text;
        }

        public async Task<string> GetUserLanguage(string input)
        {
            string route = RouteConstants.Detect + endOfRoute;
            Translate[] translate = await SendRequestToTranslateApi(route, input);

            return translate[0]?.DetectedLanguage.Language;
        }

        private async Task<Translate[]> SendRequestToTranslateApi(string route, string input)
        {
            string requestBody = Serialize(input);
            string result = await SendRequest(route, requestBody);
            return Deserialize(result);
        }

        private Translate[] Deserialize(string translateString)
        {
            Translate[] translate = null;
            try
            {
                translate = JsonConvert.DeserializeObject<Translate[]>(translateString);
            }
            catch (Exception ex)
            {
                Debugger.Break(); Log.Error(ex, ex.Message);
            }

            return translate;
        }

        private string Serialize(string input)
        {
            object[] body = new object[] { new { Text = input } };
           return JsonConvert.SerializeObject(body);
        }

        private async Task<string> SendRequest(string route, string requestBody)
        {
            using HttpClient client = new HttpClient();
            using HttpRequestMessage request = new HttpRequestMessage();

            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(_config.TranslatingConfig.EndPoint + route);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", _config.TranslatingConfig.SubscriptionKey);
            request.Headers.Add("Ocp-Apim-Subscription-Region", _config.TranslatingConfig.Region);

            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
