using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;

namespace MyTestBot.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly BotService _botService;

        public BotController(BotService botService)
        {
            _botService = botService;
        }

        [HttpGet]
        public string Get()
        {
            return "Bot controller";
        }

        //todo later add token
        [HttpPost]
        public async Task<OkResult> Post(JObject incomingObject)
        {
            Update update = incomingObject.ToObject<Update>();

            if (update != null)
            {
                await _botService.Handle(update);
            }

            return Ok();
        }
    }
}