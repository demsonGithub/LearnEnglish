namespace Demkin.Listen.Domain.ValueObjects
{
    public class MultipleLanguageTitle : ValueObject
    {
        public string ChineseTitle { get; private set; }

        public string Englisht { get; private set; }

        private MultipleLanguageTitle()
        {
        }

        public MultipleLanguageTitle(string chineseTitle, string englisht)
        {
            ChineseTitle = chineseTitle;
            Englisht = englisht;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return new object[] { ChineseTitle, Englisht };
        }
    }
}