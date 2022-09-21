namespace Demkin.System.Domain.AggregateModels
{
    public class RoleModuleRelation : Entity<long>
    {
        public long RoleId { get; private set; }

        public long ModulePermissionId { get; private set; }

        private RoleModuleRelation()
        { }

        public RoleModuleRelation(long roleId, long modulePermissionId)
        {
            Id = IdGenerateHelper.Instance.GenerateId();
            RoleId = roleId;
            ModulePermissionId = modulePermissionId;
        }
    }
}