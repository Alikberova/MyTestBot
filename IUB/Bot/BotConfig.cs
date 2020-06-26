using IUB.Translate;

namespace IUB
{
    public class BotConfig
    {
        public string WebsiteUrl { get; set; }

        public TelegramConfig TelegramConfig { get; set; }

        public TranslateConfig TranslateConfig { get; set; }
    }
}
