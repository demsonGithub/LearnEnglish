using Demkin.System.Domain.Enum;

namespace Demkin.System.Domain.Entities
{
    public class ModulePermission : Entity<long>
    {
        public long ParentId { get; private set; }

        public string Name { get; private set; }

        public string Url { get; private set; }

        public string? Icon { get; private set; }

        public LinkType LinkType { get; private set; }

        private ModulePermission()
        { }

        public ModulePermission(long parentId, string name, string url, string? icon, LinkType linkType)
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