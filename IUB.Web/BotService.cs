using Microsoft.Extensions.Configuration;
using IUB.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using IUB.Config;

namespace IUB.Web
{
    public class BotService
    {
        private readonly CommandService _commandService;

        public BotService(CommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task Handle(Update update)
        {
            TelegramBotClient client = await Bot.GetBotClientAsync();
            IReadOnlyList<Command> commands = Bot.Commands;

            Message message = update?.Message;
            CallbackQuery callBack = update?.CallbackQuery;
            long chatId = update?.Message?.Chat?.Id ?? update.CallbackQuery.Message.Chat.Id;

            if (!IsMe(message, client)) return;

            foreach (Command command in commands)
            {
                if (message != null && command.Contains(message))
                {
                    await client.SendChatActionAsync(chatId, ChatAction.Typing);
                    await command.Execute<Message>(update, client);
                    break;
                }
                else if (callBack != null)
                {
                    List<string> innerCommands = _commandService.ToStringInnerCommands(command.InnerCommands);

                    if (command.Contains(callBack))
                    {
                        await client.SendChatActionAsync(chatId, ChatAction.Typing);
                        await command.Execute<CallbackQuery>(update, client);
                        break;
                    }
                        
                    else if (command.InnerCommands != null && innerCommands.Contains(callBack.Data))
                        {
                        await client.SendChatActionAsync(chatId, ChatAction.Typing);
                        await command.Execute<CallbackQuery>(update, client);
                        break;
                    }
                }
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
                    long chatId = message.Chat.Id;
                    client.SendTextMessageAsync(chatId, "Sorry, I'm under development and not ready yet");
                    return false;
                }
            }
            return true;
        }
    }
}
