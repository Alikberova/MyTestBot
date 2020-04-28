namespace MyTestBot.TelegramModels
{
    public class TelegramMessageEntity
    {
        public string Type { get; set; }

        public int Offset { get; set; }

        public int Length { get; set; }

        public string Url { get; set; }

        public TelegramUser User { get; set; }

        public string Language { get; set; }
    }
}