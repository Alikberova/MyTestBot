namespace MyTestBot.TelegramModels
{
    public interface ITelegramUpdateData
    {
        TelegramMessage GetMessage();

        string GetText();
    }
}