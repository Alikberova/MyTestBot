namespace MyTestBot.TelegramModels
{
    public class TelegramInlineKeyboardButton
    {
        public string Text { get; set; } //only not opt

        public string Url { get; set; }

        public TelegramLoginUrl Login_Url { get; set; }

        public string CallbackData { get; set; }

        public string Switch_Inline_Query { get; set; }

        public string Switch_Inline_Query_Current_Chat { get; set; }

        public TelegramCallbackGame Callback_Game { get; set; }

        public bool? Pay { get; set; }
    }
}