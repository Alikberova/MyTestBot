using MyTestBot.Commands.Enums;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MyTestBot.BoredApi
{
    public class ActivityService
    {
        private delegate string RemoveLastInt(string value);

        public async Task<ActivityModel> GetActivityContent(ActivityModel activity)
        {
            try
            {
                UriBuilder uriBuilder = new UriBuilder("https://www.boredapi.com/api/activity");

                var apiObjectPropertyValue = activity.GetType()
                    .GetProperties()
                    .Where(p => p.PropertyType != typeof(DateTime) && p.GetValue(activity) != null)
                    .Where(p =>
                    {
                        if (p.PropertyType == typeof(PriceEnum))
                        {
                            return (PriceEnum)p.GetValue(activity) != PriceEnum.Unspecified;
                        }

                        return true;
                    });

                if (apiObjectPropertyValue.Count() > 0)
                {
                    NameValueCollection query = FormQuery(uriBuilder, activity);

                    var queryString = string.Empty;

                    queryString = query?.ToString();

                    if (!string.IsNullOrEmpty(queryString))
                    {
                        uriBuilder.Query = queryString;
                    }
                }

                using HttpClient httpClient = new HttpClient();

                string url = uriBuilder.ToString().ToLower();

                var response = httpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode) Debugger.Break();

                //var ex = "{\"activity\":\"Pull a harmless prank on one of your friends\",\"type\":\"social\"," +
                //    "\"participants\":1,\"price\":0.25,\"link\":\"\",\"key\":\"1288934\",\"accessibility\":0.2}";

                var content = await response.Content.ReadAsStringAsync();

                RemoveLastInt removeLastInt = RemoveLastIntMethod;

                content = Regex.Replace(content, "\"price\":0.\\d+", m => removeLastInt(m.Value));
                content = Regex.Replace(content, "\"accessibility\":0.\\d+", m => removeLastInt(m.Value));

                var activityResult = JsonConvert.DeserializeObject<ActivityModel>(content);

                return activityResult;

            }
            catch (Exception ex)
            {
                Debugger.Break(); Log.Error(ex, ex.Message);
            }

            return null;
        }

        private string RemoveLastIntMethod(string value)
        {
            if (value.Contains("price"))
            {
                value = value.Replace("0.", "");
                if (value.Length == 10)
                {
                    value = value.Remove(9);
                }
            }
            //for accessibility
            else
            {
                if (value.Length == 20)
                {
                    value = value.Remove(19);
                }
            }

            return value;
        }

        private NameValueCollection FormQuery(UriBuilder uriBuilder, ActivityModel activity)
        {
            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            var properties = activity.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                try
                {
                    if (prop.Name == nameof(ActivityModel.Activity) || prop.Name == "Id" ||
                        prop.Name == nameof(ActivityModel.CreatedDate) || prop.Name == nameof(ActivityModel.ModifiedDate))
                    {
                        continue;
                    }

                    if (prop.Name == nameof(ActivityModel.Price))
                    {
                        int value = (int)prop.GetValue(activity);
                        if (value == 0)
                        {
                            continue;
                        }
                        else
                        {
                            query.Add(prop.Name, "0." + value.ToString());
                        }
                    }
                    else
                    {
                        query.Add(prop.Name, prop.GetValue(activity)?.ToString());
                    }
                    
                }
                catch (Exception ex)
                {
                    Debugger.Break(); Log.Error(ex, ex.Message);
                }
            }
            return query;
        }
    }
}
