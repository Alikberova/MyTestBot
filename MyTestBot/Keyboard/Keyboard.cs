using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyTestBot.Keyboard
{
    public class Keyboard
    {
        public static double CountOfButtonsPerRow = 3;

        public static List<string> TypeOfActivityList { get; } = new List<string>() { "education", "recreational",
                    "social", "diy", "charity", "cooking", "relaxation", "music", "busywork" };
    }
}
