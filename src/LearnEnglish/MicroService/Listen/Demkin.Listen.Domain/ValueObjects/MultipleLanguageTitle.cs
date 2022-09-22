namespace Demkin.Listen.Domain.ValueObjects
{
    public class MultipleLanguageTitle : ValueObject
    {
        public string ChineseTitle { get; private set; }

        public string EnglishTitle { get; private set; }

        private MultipleLanguageTitle()
        {
        }

        public MultipleLanguageTitle(string chineseTitle, string english)
        {
            ChineseTitle = chineseTitle;
            EnglishTitle = english;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return new object[] { ChineseTitle, EnglishTitle };
        }
    }
}