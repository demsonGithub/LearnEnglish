using Demkin.Domain.Abstraction;

namespace Demkin.Infrastructure.Core
{
    public interface IDbContextFactory : IDenpendencySingleton
    {
        /// <summary>
        /// 设置主从库，创建一个私有字段保存，需要在构造器中执行初始化
        /// </summary>
        void SetMasterAndSlaveDb();

        /// <summary>
        /// 生成主库连接
        /// </summary>
        /// <returns></returns>
        MyDbContext CreateMasterDbContext();

        /// <summary>
        /// 创建一个从库的连接
        /// </summary>
        /// <returns></returns>
        MyDbContext CreateSlaveDbContext();
    }
}