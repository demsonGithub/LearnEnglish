using Demkin.Domain.Abstraction;

namespace Demkin.System.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Province { get; private set; }
        public string City { get; private set; }
        public string Area { get; private set; }
        public string Street { get; private set; }

        public Address(string province, string city, string area, string street)
        {
            Province = province;
            City = city;
            Area = area;
            Street = street;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Province;
            yield return City;
            yield return Area;
            yield return Street;
        }
    }
}