namespace Demkin.System.Domain.AggregateModels
{
    public class RoleModuleRelation : Entity<long>
    {
        public long RoleId { get; private set; }

        public long ModulePermissionId { get; private set; }

        private RoleModuleRelation()
        { }

        public static RoleModuleRelation Create(long roleId, long modulePermissionId)
        {
            RoleModuleRelation item = new RoleModuleRelation()
            {
                Id = IdGenerateHelper.Instance.GenerateId(),
                RoleId = roleId,
                ModulePermissionId = modulePermissionId
            };
            item.AddDomainEvent(new RoleModuleRelationCreatedDomainEvent(item));
            return item;
        }
    }
}