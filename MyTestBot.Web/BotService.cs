using MyTestBot.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MyTestBot.Web
{
    public class BotService
    {
        public async Task Handle(Update update)
        {
            var botClient = await Bot.GetBotClientAsync();
            var commands = Bot.Commands;

            try
            {
                var message = update?.Message;
                var callBack = update?.CallbackQuery;

                foreach (Command command in commands)
                {
                    if (message != null && command.Contains(message))
                    {
                        await command.Execute<Message>(update, botClient);
                        break;
                    }
                    else if (callBack != null)
                    {
                        if (command.Contains(callBack))
                        {
                            await command.Execute<CallbackQuery>(update, botClient);
                            break;
                        }
                        else if (command.InnerNames != null && (bool)command.InnerNames?.Contains(callBack.Data))
                        {
                            await command.Execute<CallbackQuery>(update, botClient);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }
    }
}
