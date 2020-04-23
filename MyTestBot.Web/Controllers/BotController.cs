using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTestBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTestBot.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly BotConfig _botConfig;
        private readonly BotService _botService;

        public BotController(BotConfig botConfig, BotService botService)
        {
            _botConfig = botConfig;
            _botService = botService;
        }

        [HttpGet]
        public string Get()
        {
            return "Bot controller";
        }

        [HttpPost]
        public async Task Post([FromBody]TelegramUpdate telegramUpdate) //todo [FromBody]
        {
            await _botService.OnMessage(telegramUpdate);
        }
    }
}