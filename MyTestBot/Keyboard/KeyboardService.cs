using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyTestBot.Keyboard
{
    public class KeyboardService : Keyboard
    {
        private const double _buttonsCountPerLine = 3;

        public override ReplyKeyboardMarkup ReplyKeyboardMarkup(List<string> keyboardButtonNames, 
            bool resizeKeyboard = true, bool oneTimeKeyboard = false)
        {
            KeyboardButton[][] keyboard = GetKeyboard(keyboardButtonNames);

            ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup
            {
                Keyboard = keyboard,
                ResizeKeyboard = resizeKeyboard,
                OneTimeKeyboard = oneTimeKeyboard
            };

            return replyKeyboardMarkup;
        }

        private KeyboardButton[][] GetKeyboard(List<string> keyboardButtonNames)
        {
            var rowsCount = (int)Math.Ceiling(keyboardButtonNames.Count / _buttonsCountPerLine);
            KeyboardButton[][] keyboardButtons = new KeyboardButton[rowsCount][];

            if (keyboardButtonNames.Count > _buttonsCountPerLine)
            {

                for (int i = 0; i < keyboardButtons.Length; i++)
                {
                    if (keyboardButtonNames.Count < _buttonsCountPerLine)
                    {
                        keyboardButtons.SetValue(GetSingleLineButtons(keyboardButtonNames), i);
                        break;
                    }

                    List<string> buttonsNamesCountForLine = keyboardButtonNames.GetRange(0, (int)_buttonsCountPerLine);
                    keyboardButtonNames.RemoveRange(0, (int)_buttonsCountPerLine);
                    keyboardButtons.SetValue(GetSingleLineButtons(buttonsNamesCountForLine), i);
                }
            }
            else
            {
                keyboardButtons.SetValue(GetSingleLineButtons(keyboardButtonNames), 0);
            }
            
            return keyboardButtons;
        }

        private KeyboardButton[] GetSingleLineButtons(List<string> keyboardButtonNames)
        {
            KeyboardButton[] keyboardButtons = new KeyboardButton[keyboardButtonNames.Count];
            foreach (var keyboardButtonName in keyboardButtonNames)
            {
                KeyboardButton keyboardButton = new KeyboardButton() { Text = keyboardButtonName };
                keyboardButtons.SetValue(keyboardButton, keyboardButtonNames.IndexOf(keyboardButtonName));
            }

            return keyboardButtons;
        }
    }
}
