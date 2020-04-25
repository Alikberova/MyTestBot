namespace MyTestBot.Models
{
    public class User
    {
        public int Id { get; set; }

        public bool Is_Bot { get; set; }

        public string First_Name { get; set; }

        public string Last_Mame { get; set; }

        public string Username { get; set; }

        public string Language_Code { get; set; }

        public bool Can_Join_Groups { get; set; }

        public bool Can_Read_All_Group_Messages { get; set; }

        public bool Supports_Inline_Queries { get; set; }
    }
}