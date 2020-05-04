using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyTestBot.Commands;
using MyTestBot.TelegramModels;

namespace MyTestBot.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly BotConfig _botConfig;

        public BotController(BotConfig botConfig)
        {
            _botConfig = botConfig;
        }

        [HttpGet]
        public string Get()
        {
            return "Bot controller";
        }

        [HttpPost]
        public async Task<OkResult> Post([FromBody]TelegramUpdate update)
        {
            if (update == null) return Ok();
            
            var botClient = await Bot.GetBotClientAsync();

            var commands = Bot.Commands;

            try
            {
                var message = update.Message;
                var callBack = update.Callback_Query;

                foreach (Command command in commands)
                {
                    var inners = command.InnerNames;

                    if (message != null && command.Contains(message))
                    {
                        if (inners?.Count > 0)
                        {
                            await command.Execute<TelegramMessage>(update, botClient, true);
                            break;
                        }

                        await command.Execute<TelegramMessage>(update, botClient, false);
                        break;
                    }
                    else if (callBack != null)
                    {
                        if (command.Contains(callBack))
                        {
                            await command.Execute<TelegramCallbackQuery>(update, botClient, false);
                            break;
                        }
                        else if(inners?.Count > 0)
                        {
                            foreach (var inner in inners)
                            {
                                if (callBack.Data == inner)
                                {
                                    await command.Execute<TelegramCallbackQuery>(update, botClient, true);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
            return Ok();
        }
    }
}