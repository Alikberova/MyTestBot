namespace IUB.Translating
{
    public class Translate
    {
        public Translation[] Translations { get; set; }

        public DetectedLanguage DetectedLanguage { get; set; }
    }

    public class Translation
    {
        public string Text { get; set; }

        public string To { get; set; }
    }

    public class DetectedLanguage
    {
        public string Language { get; set; }

        public decimal Score { get; set; }
    }
}
