namespace MyTestBot.TelegramModels
{
    public class TelegramMessage
    {
        public int Message_Id { get; set; } //not opt

        public TelegramUser From { get; set; }

        public TelegramChat Chat { get; set; } //not opt

        public int Date { get; set; } //not opt //todo Unix time

        public TelegramMessage Reply_To_Message { get; set; }

        public int? Edit_Date { get; set; }

        public string Text { get; set; }

        public TelegramInlineKeyboardMarkup Inline_Keyboard_Markup { get; set; }

        public TelegramMessageEntity[] Caption_Entities { get; set; }

        public TelegramMessageEntity[] Entities { get; set; }

        public TelegramSticker Sticker { get; set; }

        public TelegramUser Forward_From { get; set; }

        public TelegramChat Forward_From_Chat { get; set; }
        
        public int? Forward_From_Message_Id { get; set; }
        
        public string Forward_Signature { get; set; }

        public string Forward_Sender_Name { get; set; }

        public int? Forward_Date { get; set; } //unix

        public string Media_Group_Id { get; set; }

        public string Author_Signature { get; set; }

        public TelegramAudio Audio { get; set; }

        public TelegramDocument Document { get; set; }

        public TelegramAnimation Animation { get; set; }

        public TelegramGame Game { get; set; }

        public TelegramPhotoSize[] Photo { get; set; }

        public Video Video { get; set; }

        public Voice Voice { get; set; }

        public VideoNote Video_Note { get; set; }

        public string Caption { get; set; }

        public TelegramContact Contact { get; set; }

        public TelegramLocation Location { get; set; }

        public Venue Venue { get; set; }

        public TelegramPoll Poll { get; set; }

        public TelegramDice Dice { get; set; }

        public TelegramUser[] New_Chat_Members { get; set; }

        public TelegramUser Left_Chat_Member { get; set; }

        public string New_Chat_Title { get; set; }

        public TelegramPhotoSize[] New_Chat_Photo { get; set; }

        public bool? Delete_Chat_Photo { get; set; }

        public bool? Group_Chat_Created { get; set; }

        public bool? Supergroup_Chat_Created { get; set; }

        public bool? Channel_Chat_Creted { get; set; }

        public int? Migrate_To_Chat_Id { get; set; }

        public int? Migrate_From_Chat_Id { get; set; }

        public TelegramMessage Pinned_Message { get; set; }

        public TelegramInvoice Invoice { get; set; }

        public SuccessfulPayment Successful_Payment { get; set; }

        public string Connected_Website { get; set; }

        public TelegramPassportData Passport_Data { get; set; }
    }
}