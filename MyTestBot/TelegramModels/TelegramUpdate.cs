namespace MyTestBot.TelegramModels
{
    public class TelegramUpdate
    {
        public int Update_Id { get; set; }

        public TelegramMessage Message { get; set; }

        public TelegramInlineQuery Inline_Query { get; set; }

        public TelegramCallbackQuery Callback_Query { get; set; }
    }
}
