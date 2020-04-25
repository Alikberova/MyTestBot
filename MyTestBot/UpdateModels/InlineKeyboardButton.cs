namespace MyTestBot.Models
{
    public class InlineKeyboardButton
    {
        public string Text { get; set; } //only not opt

        public string Url { get; set; }

        public LoginUrl Login_Url { get; set; }

        public string CallbackData { get; set; }

        public string Switch_Inline_Query { get; set; }

        public string Switch_Inline_Query_Current_Chat { get; set; }

        public CallbackGame Callback_Game { get; set; }

        public bool? Pay { get; set; }
    }
}