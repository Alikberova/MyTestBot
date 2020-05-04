namespace MyTestBot.TelegramModels
{
    public class TelegramMessage : ITelegramUpdateData
    {
        TelegramMessage ITelegramUpdateData.GetMessage()
        {
            return TelegramMessageObject;
        }

        public string GetText()
        {
            return Text;
        }

        public TelegramMessage TelegramMessageObject { get; set; }  //todo fix null

        public int Message_Id { get; set; } //not opt

        public TelegramUser From { get; set; }

        public TelegramChat Chat { get; set; } //not opt

        public int Date { get; set; } //not opt //todo Unix time

        public TelegramMessage Reply_To_Message { get; set; }

        public string Text { get; set; }

        public TelegramSticker Sticker { get; set; }

        public TelegramInlineKeyboardMarkup Inline_Keyboard_Markup { get; set; }

        public TelegramMessageEntity[] Caption_Entities { get; set; }

        public TelegramMessageEntity[] Entities { get; set; }

        public TelegramUser Forward_From { get; set; }

        public TelegramChat Forward_From_Chat { get; set; }
        
        public int? Forward_From_Message_Id { get; set; }
        
        public string Forward_Signature { get; set; }

        public string Forward_Sender_Name { get; set; }

        public int? Forward_Date { get; set; } //unix
    }
}