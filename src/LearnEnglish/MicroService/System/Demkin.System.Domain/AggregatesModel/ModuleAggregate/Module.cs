using Demkin.System.Domain.Enum;

namespace Demkin.System.Domain.AggregatesModel.ModuleAggregate
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

        public Module(long parentId, string name, string url, string? icon, LinkType linkType)
        {
            Id = IdGenerateHelper.Instance.GenerateId();
            ParentId = parentId;
            Name = name;
            Url = url;
            Icon = icon;
            LinkType = linkType;
        }
    }
}