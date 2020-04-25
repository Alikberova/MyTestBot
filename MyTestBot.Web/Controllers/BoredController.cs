using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyTestBot.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoredController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            HttpClient httpClient = new HttpClient();
            var resp = httpClient.GetAsync("https://www.boredapi.com/api/activity?accessibility=1").Result;
            var cont = await resp.Content.ReadAsStringAsync();
            return cont;
        }
    }
}