using Demkin.Infrastructure.Core;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Demkin.Listen.Infrastructure
{
    public class DbContextFactory
    {
        /*
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        private (MyDbContext masterDb, List<MyDbContext> slaveDbs) _allDbInfos;

        public DbContextFactory(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;

            SetMasterAndSlaveDb();
        }

        public void SetMasterAndSlaveDb()
        {
            List<MyDbContext> slaveList = new List<MyDbContext>();
            ListenDbContext master = new ListenDbContext(_mediator, _configuration.GetSection("DbConnection:MasterDb").Value);

            List<DbInformation> slaveDbInfos = _configuration.GetSection("DbConnection:SlaveDb").GetChildren().Select(x => new DbInformation
            {
                ContextName = x.GetSection("ContextName").Value,
                DataBaseType = x.GetSection("DataBaseType").Value == null ? DataBaseType.Sqlite : (DataBaseType)Convert.ToInt32(x.GetSection("DataBaseType").Value),
                ConnectionString = x.GetSection("ConnectionString").Value,
                HitRate = x.GetSection("HitRate").Value == null ? 0 : Convert.ToInt32(x.GetSection("HitRate").Value)
            }).ToList();

            foreach (var item in slaveDbInfos)
            {
                ListenDbContext slaveDb = new ListenDbContext(_mediator, item.ConnectionString);
                if (slaveDb.Database.CanConnect())
                {
                    slaveList.Add(slaveDb);
                }
            }
            if (slaveList.Count == 0)
            {
                slaveList.Add(master);
            }
            _allDbInfos = (master, slaveList);
        }

        public MyDbContext CreateMasterDbContext()
        {
            return _allDbInfos.masterDb;
        }

        public MyDbContext CreateSlaveDbContext()
        {
            if (_allDbInfos.slaveDbs.Count == 0)
                throw new DomainException("至少需要一个可用的从库");

            Random random = new Random();
            // 随机策略
            int randNum = random.Next(_allDbInfos.slaveDbs.Count);
            return _allDbInfos.slaveDbs[randNum];
        }
        */
    }
}