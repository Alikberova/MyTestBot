using IUB.Translating;

namespace IUB
{
    public class BotConfig
    {
        public string WebsiteUrl { get; set; }

        public TelegramConfig TelegramConfig { get; set; }

        public TranslatingConfig TranslatingConfig { get; set; }
    }
}
