using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
                var msg = update.Message;
                var callBack = update.Callback_Query;

                foreach (Command command in commands)
                {
                    var inners = command.InnerNames;

                    if (msg != null && command.Contains(msg))
                    {
                        if (inners?.Count > 0)
                        {
                            await command.Execute(msg, botClient, true);
                            break;
                        }

                        await command.Execute(msg, botClient, false);
                        break;
                    }
                    else if (callBack != null)
                    {
                        if (command.Contains(callBack))
                        {
                            await command.Execute(callBack, botClient, false);
                            break;
                        }
                        else if(inners?.Count > 0)
                        {
                            foreach (var inner in inners)
                            {
                                if (callBack.Data == inner)
                                {
                                    await command.Execute(callBack, botClient, true);
                                    break;
                                }

                                //break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
            //foreach (Command command in commands)
            //{
            //    if (command.Contains(message))
            //    {
            //        await command.Execute(message, botClient, false);
            //        break;
            //    }
            //    else if (command.InnerNames?.Count > 0)
            //    {
            //        await command.Execute(message, botClient, isInnerCommand: true);
            //        break;
            //    }
            //    else if (command.Contains(telegramUpdate.Callback_Query))
            //    {
            //        //await command.Execute(telegramUpdate.Callback_Query, botClient, isInnerCommand: false);
            //        break;
            //    }
            //}
            return Ok();
        }
    }
}