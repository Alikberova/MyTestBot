using MyTestBot.Translate;

namespace MyTestBot
{
    public class BotConfig
    {
        public string WebsiteUrl { get; set; }

        public TelegramConfig TelegramConfig { get; set; }

        public TranslateConfig TranslateConfig { get; set; }
    }
}
