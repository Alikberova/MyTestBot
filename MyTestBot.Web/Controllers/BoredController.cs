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
        private HttpClient _httpClient;

        public BoredController()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri("https://www.boredapi.com/api/") };
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var response = _httpClient.GetAsync("activity").Result;
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        [HttpPost]
        public async Task<string> Post() //todo params
        {
            var response = _httpClient.GetAsync("activity").Result;
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}