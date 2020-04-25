using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyTestBot.BoredApi
{
    public class BoredApiService
    {
        //todo Get Activity, accessibility, type etc

        public async Task<Bored> GetContent()
        {
            HttpClient httpClient = new HttpClient();
            var resp = httpClient.GetAsync("https://900bf880.ngrok.io/bored").Result;
            var cont = await resp.Content.ReadAsStringAsync();

            Bored bored = null;
            try
            {
                bored = JsonConvert.DeserializeObject<Bored>(cont);
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
