namespace MyTestBot.TelegramModels
{
    public class TelegramChatPermissions
    {
        public bool Can_Send_Messages { get; set; }

        public bool Can_Send_Media_Messages { get; set; }

        public bool Can_Send_Polls { get; set; }

        public bool Can_Send_Other_Messages { get; set; }

        public bool Can_Add_Web_Page_Previews { get; set; }

        public bool Can_Change_Info { get; set; }

        public bool Can_Invite_Users { get; set; }

        public bool Can_Pin_Messages { get; set; }
    }
}