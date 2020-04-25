namespace MyTestBot.Models
{
    public class Message
    {
        public int Message_Id { get; set; } //not opt

        public User From { get; set; }

        public Chat Chat { get; set; } //not opt

        public int Date { get; set; } //not opt //todo Unix time

        public Message Reply_To_Message { get; set; }

        public int? Edit_Date { get; set; }

        public string Text { get; set; }

        public InlineKeyboardMarkup Inline_Keyboard_Markup { get; set; }

        public MessageEntity[] Caption_Entities { get; set; }

        public MessageEntity[] Entities { get; set; }

        public Sticker Sticker { get; set; }

        public User Forward_From { get; set; }

        public Chat Forward_From_Chat { get; set; }
        
        public int? Forward_From_Message_Id { get; set; }
        
        public string Forward_Signature { get; set; }

        public string Forward_Sender_Name { get; set; }

        public int? Forward_Date { get; set; } //unix

        public string Media_Group_Id { get; set; }

        public string Author_Signature { get; set; }

        public Audio Audio { get; set; }

        public Document Document { get; set; }

        public Animation Animation { get; set; }

        public Game Game { get; set; }

        public PhotoSize[] Photo { get; set; }

        public Video Video { get; set; }

        public Voice Voice { get; set; }

        public VideoNote Video_Note { get; set; }

        public string Caption { get; set; }

        public Contact Contact { get; set; }

        public Location Location { get; set; }

        public Venue Venue { get; set; }

        public Poll Poll { get; set; }

        public Dice Dice { get; set; }

        public User[] New_Chat_Members { get; set; }

        public User Left_Chat_Member { get; set; }

        public string New_Chat_Title { get; set; }

        public PhotoSize[] New_Chat_Photo { get; set; }

        public bool? Delete_Chat_Photo { get; set; }

        public bool? Group_Chat_Created { get; set; }

        public bool? Supergroup_Chat_Created { get; set; }

        public bool? Channel_Chat_Creted { get; set; }

        public int? Migrate_To_Chat_Id { get; set; }

        public int? Migrate_From_Chat_Id { get; set; }

        public Message Pinned_Message { get; set; }

        public Invoice Invoice { get; set; }

        public SuccessfulPayment Successful_Payment { get; set; }

        public string Connected_Website { get; set; }

        public PassportData Passport_Data { get; set; }
    }
}