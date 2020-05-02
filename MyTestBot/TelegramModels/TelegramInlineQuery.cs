namespace MyTestBot.TelegramModels
{
    public class TelegramInlineQuery
    {
        public string Id { get; set; }

        public TelegramUser User { get; set; }

        public object Location { get; set; }

        public string Query { get; set; }

        public string Offset { get; set; }
    }
}