using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyTestBot.Keyboard
{
    public abstract class Keyboard
    {
        public abstract ReplyKeyboardMarkup ReplyKeyboardMarkup(List<string> keyboardButtonNames, bool resizeKeyboard,
            bool oneTimeKeyboard);
    }
}
