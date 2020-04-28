namespace MyTestBot.TelegramModels
{
    public class TelegramCallbackGame
    {
        public int User_Id { get; set; }

        public int Score { get; set; }

        public bool? Force { get; set; }

        public bool? Disable_Edit_Message { get; set; }

        public int? Chat_Id { get; set; }

        public int? Message_Id { get; set; }

        public string Inline_Message_Id { get; set; }
    }
}