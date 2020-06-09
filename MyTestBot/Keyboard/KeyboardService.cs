using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyTestBot.Keyboard
{
    public class KeyboardService
    {
        private const double CountOfButtonsPerRow = 3;

        public InlineKeyboardMarkup GetKeyboard(List<string> keyboardButtonNames)
        {
            var rowsCount = (int)Math.Ceiling(keyboardButtonNames.Count / CountOfButtonsPerRow);
            InlineKeyboardButton[][] keyboardButtons = new InlineKeyboardButton[rowsCount][];

            if (keyboardButtonNames.Count > CountOfButtonsPerRow)
            {

                for (int i = 0; i < keyboardButtons.Length; i++)
                {
                    if (keyboardButtonNames.Count < CountOfButtonsPerRow)
                    {
                        keyboardButtons.SetValue(SingleRowButtons(keyboardButtonNames), i);
                        break;
                    }

                    List<string> buttonsNamesCountForLine = keyboardButtonNames.GetRange(0,
                        (int)CountOfButtonsPerRow);
                    keyboardButtonNames.RemoveRange(0, (int)CountOfButtonsPerRow);
                    keyboardButtons.SetValue(SingleRowButtons(buttonsNamesCountForLine), i);
                }
            }
            else
            {
                keyboardButtons.SetValue(SingleRowButtons(keyboardButtonNames), 0);
            }

            InlineKeyboardMarkup replyKeyboardMarkup = new InlineKeyboardMarkup(keyboardButtons);
            return replyKeyboardMarkup;
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
