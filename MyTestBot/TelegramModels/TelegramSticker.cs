namespace MyTestBot.TelegramModels
{
    public class TelegramSticker
    {
        public string File_Id { get; set; }

        public string File_Unique_Id { get; set; }

        public int Width { get; set; }

        public int Lenght { get; set; }

        public bool Is_Animated { get; set; }

        public object Thumb { get; set; }

        public string Emoji { get; set; }

        public string Set_Name { get; set; }

        public object Mask_Position { get; set; }

        public int File_size { get; set; }
    }
}