using Microsoft.Extensions.Configuration;
using MyTestBot.Commands;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTestBot.Web
{
    public class BotService
    {
        public async Task Handle(Update update)
        {
            TelegramBotClient client = await Bot.GetBotClientAsync();
            IReadOnlyList<Command> commands = Bot.Commands;

            try
            {
                Message message = update?.Message;
                CallbackQuery callBack = update?.CallbackQuery;

                if (!IsMe(message, client)) return;

                foreach (Command command in commands)
                {
                    if (message != null && command.Contains(message))
                    {
                        await command.Execute<Message>(update, client);
                        break;
                    }
                    else if (callBack != null)
                    {
                        if (command.Contains(callBack))
                        {
                            await command.Execute<CallbackQuery>(update, client);
                            break;
                        }
                        else if (command.InnerNames != null && (bool)command.InnerNames?.Contains(callBack.Data))
                        {
                            await command.Execute<CallbackQuery>(update, client);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Break(); Log.Error(ex, ex.Message);
            }
        }

        private bool IsMe(Message message, TelegramBotClient client)
        {
            if (message == null) return true;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                BotConfig config = Startup.StaticConfig.GetSection("BotConfig").Get<BotConfig>();

                if (message?.From.Id != config.TelegramConfig.MyProfileId)
                {
                    client.SendTextMessageAsync(message.Chat.Id, "Sorry, I'm under development and not ready yet");
                    return false;
                }
            }
            return true;
        }
    }
}
