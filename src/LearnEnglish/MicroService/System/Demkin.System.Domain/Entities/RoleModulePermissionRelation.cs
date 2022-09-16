namespace Demkin.System.Domain.Entities
{
    public class RoleModulePermissionRelation : Entity<long>
    {
        public long RoleId { get; private set; }

        public long ModulePermissionId { get; private set; }

        private RoleModulePermissionRelation()
        { }

        public RoleModulePermissionRelation(long roleId, long modulePermissionId)
        {
            Id = IdGenerateHelper.Instance.GenerateId();
            RoleId = roleId;
            ModulePermissionId = modulePermissionId;
        }
    }
}