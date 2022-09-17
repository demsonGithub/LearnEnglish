using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.System.Infrastructure
{
    public static class SeedData
    {
        private static SystemDbContext _dbContext;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _dbContext = (SystemDbContext)serviceProvider.GetService(typeof(SystemDbContext)) ?? throw new ArgumentNullException(nameof(SystemDbContext));

            InitializeData();
        }

        private static async void InitializeData()
        {
            #region 判断是否存在数据

            if (_dbContext.Users.Any())
            {
                Console.WriteLine("User表中存在数据");
                return;
            }
            if (_dbContext.Roles.Any())
            {
                Console.WriteLine("Role表中存在数据");
                return;
            }
            if (_dbContext.UserRoleRelations.Any())
            {
                Console.WriteLine("UserRoleRelation表中存在数据");
                return;
            }
            if (_dbContext.Modules.Any())
            {
                Console.WriteLine("Module表中存在数据");
                return;
            }
            if (_dbContext.RoleModuleRelations.Any())
            {
                Console.WriteLine("RoleModuleRelation表中存在数据");
                return;
            }

            #endregion 判断是否存在数据

            Address address = new Address("湖北省", "武汉市", "东西湖区", "三店中路");
            var user = new User("admin", "123456", address);
            _dbContext.Users.Add(user);

            var role = new Role("超级管理员", "系统最大的权限者");
            _dbContext.Roles.Add(role);

            var userRoleRelation = new UserRoleRelation(user.Id, role.Id);
            _dbContext.UserRoleRelations.Add(userRoleRelation);

            await _dbContext.SaveEntitiesAsync();

            Console.WriteLine("完成初始化数据");
        }
    }
}