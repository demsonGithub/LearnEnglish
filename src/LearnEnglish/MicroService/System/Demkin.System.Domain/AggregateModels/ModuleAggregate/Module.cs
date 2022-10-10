using Demkin.System.Domain.Enum;

namespace Demkin.System.Domain.AggregateModels
{
    public class Module : Entity<long>, IAggregateRoot
    {
        public long ParentId { get; private set; }

        public string Name { get; private set; }

        public string Url { get; private set; }

        public string? Icon { get; private set; }

        public LinkType LinkType { get; private set; }

        private Module()
        { }

        public static Module Create(long parentId, string name, string url, string? icon, LinkType linkType)
        {
            Module item = new Module()
            {
                Id = IdGenerateHelper.Instance.GenerateId(),
                ParentId = parentId,
                Name = name,
                Url = url,
                Icon = icon,
                LinkType = linkType,
            };
            item.AddDomainEvent(new ModuleCreatedDomainEvent(item));
            return item;
        }
    }
}