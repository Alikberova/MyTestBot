using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyTestBot.Keyboard
{
    public class KeyboardService
    {
        public InlineKeyboardMarkup ReplyKeyboardMarkup(List<string> keyboardButtonNames)
        {
            InlineKeyboardButton[][] keyboard = GetKeyboard(keyboardButtonNames);

            InlineKeyboardMarkup replyKeyboardMarkup = new InlineKeyboardMarkup(keyboard);
            return replyKeyboardMarkup;
        }

        private InlineKeyboardButton[][] GetKeyboard(List<string> keyboardButtonNames)
        {
            var rowsCount = (int)Math.Ceiling(keyboardButtonNames.Count / Keyboard.CountOfButtonsPerRow);
            InlineKeyboardButton[][] keyboardButtons = new InlineKeyboardButton[rowsCount][];

            if (keyboardButtonNames.Count > Keyboard.CountOfButtonsPerRow)
            {

                for (int i = 0; i < keyboardButtons.Length; i++)
                {
                    if (keyboardButtonNames.Count < Keyboard.CountOfButtonsPerRow)
                    {
                        keyboardButtons.SetValue(SingleRowButtons(keyboardButtonNames), i);
                        break;
                    }

                    List<string> buttonsNamesCountForLine = keyboardButtonNames.GetRange(0,
                        (int)Keyboard.CountOfButtonsPerRow);
                    keyboardButtonNames.RemoveRange(0, (int)Keyboard.CountOfButtonsPerRow);
                    keyboardButtons.SetValue(SingleRowButtons(buttonsNamesCountForLine), i);
                }
            }
            else
            {
                keyboardButtons.SetValue(SingleRowButtons(keyboardButtonNames), 0);
            }

            return keyboardButtons;
        }

        private InlineKeyboardButton[] SingleRowButtons(List<string> keyboardButtonNames)
        {
            InlineKeyboardButton[] keyboardButtons = new InlineKeyboardButton[keyboardButtonNames.Count];
            foreach (var keyboardButtonName in keyboardButtonNames)
            {
                InlineKeyboardButton keyboardButton = new InlineKeyboardButton() { Text = keyboardButtonName, CallbackData = keyboardButtonName };
                keyboardButtons.SetValue(keyboardButton, keyboardButtonNames.IndexOf(keyboardButtonName));
            }

            return keyboardButtons;
        }
    }
}
