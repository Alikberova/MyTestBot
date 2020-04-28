namespace MyTestBot.TelegramModels
{
    public class TelegramLoginUrl
    {
        public string Url { get; set; }

        public string Forward_Text { get; set; }

        public string Bot_Username { get; set; }

        public bool Request_Write_Access { get; set; }
    }
}