namespace Demkin.Domain.Abstraction
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetAtomicValues();
    }
}