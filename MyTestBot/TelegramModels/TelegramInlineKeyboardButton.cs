namespace MyTestBot.TelegramModels
{
    public class TelegramInlineKeyboardButton
    {
        public string Text { get; set; }

        public string Url { get; set; }

        public TelegramLoginUrl Login_Url { get; set; }

        public string CallbackData { get; set; }

        public string Switch_Inline_Query { get; set; }

        public string Switch_Inline_Query_Current_Chat { get; set; }
    }
}