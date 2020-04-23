namespace MyTestBot.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public string First_Name { get; set; }

        public string Last_Mame { get; set; }

        public string Username { get; set; }

        public object Photo { get; set; } //object ChatPhoto

        public string Description { get; set; }

        public Message Pinned_Message { get; set; }

        public ChatPermissions Permissions { get; set; }
    }
}