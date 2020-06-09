using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace MyTestBot.BoredApi
{
    public class BoredApiService
    {
        public async Task<string> GetActivityContent(BoredActivity activity)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                UriBuilder uriBuilder = new UriBuilder("https://www.boredapi.com/api/activity");

                var query = FormQuery(uriBuilder, activity);

                var queryString = string.Empty;

                try
                {
                    queryString = query?.ToString(); //todo fix throw
                }
                catch (Exception ex)
                {
                }

                if (!string.IsNullOrEmpty(queryString))
                {
                    uriBuilder.Query = queryString;
                }

                string url = uriBuilder.ToString().ToLower();

                var response = httpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode) Debugger.Break();

                var content = await response.Content.ReadAsStringAsync();
                var activityResult = JsonConvert.DeserializeObject<BoredActivity>(content);


                return activityResult.Activity;

            }
            catch (Exception ex)
            {
                Debugger.Break();
            }

            return null;
        }

        private NameValueCollection FormQuery(UriBuilder uriBuilder, BoredActivity activity)
        {
            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            var properties = activity.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                try
                {
                    if (prop.Name == "Activity") continue;
                    query.Add(prop.Name, prop.GetValue(activity)?.ToString());
                }
                catch (Exception ex)
                {
                }
            }
            return query;
        }
    }
}
