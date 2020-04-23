using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyTestBot.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HookController : ControllerBase
    {
        private readonly BotConfig _botConfig;
        private readonly ILogger<HookController> _logger;

        public HookController(ILogger<HookController> logger, BotConfig botConfig)
        {
            _logger = logger;
            _botConfig = botConfig;
        }

        [HttpGet]
        public string Get()
        {
            return "Hook controller";
        }

        [HttpGet]
        [Route("set")]
        public void Set()
        {
            var bot = new TelegramBotClient(_botConfig.Token);
            bot.SetWebhookAsync(_botConfig.Ngrok + "/bot");
        }

        [HttpGet]
        [Route("undo")]
        public void Delete()
        {
            var bot = new TelegramBotClient(_botConfig.Token);
            bot.DeleteWebhookAsync();
        }
    }
}
