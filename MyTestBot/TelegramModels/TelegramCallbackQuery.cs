namespace MyTestBot.TelegramModels
{
    public class TelegramCallbackQuery
    {
        public string Id { get; set; }

        public TelegramUser From { get; set; }

        public TelegramMessage Message { get; set; }

        public string Inline_Message_Id { get; set; }

        public string Chat_Instance { get; set; }

        public string Data { get; set; }
    }
}