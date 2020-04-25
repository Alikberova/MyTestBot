namespace MyTestBot.Models
{
    public class Sticker
    {
        public string File_Id { get; set; }

        public string File_Unique_Id { get; set; }

        public int Width { get; set; }

        public int Lenght { get; set; }

        public bool Is_Animated { get; set; }

        public PhotoSize Thumb { get; set; }

        public string Emoji { get; set; }

        public string Set_Name { get; set; }

        public MaskPosition Mask_Position { get; set; }

        public int File_size { get; set; }
    }
}