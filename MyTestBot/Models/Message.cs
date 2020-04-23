namespace MyTestBot.Models
{
    public class Message
    {
        public int Message_Id { get; set; }

        public User From { get; set; }

        public Chat Chat { get; set; }

        public int Date { get; set; } //todo Unix time

        public Message Reply_To_Message { get; set; }

        public int Edit_Date { get; set; }

        public string Text { get; set; }

        public MessageEntity[] Entities { get; set; }

        public Sticker Sticker { get; set; }
    }
}