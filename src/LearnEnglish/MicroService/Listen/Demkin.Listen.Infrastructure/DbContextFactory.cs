using Demkin.Core.Exceptions;
using Demkin.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Demkin.Listen.Infrastructure
{
    public class DbContextFactory : IDbContextFactory<ListenDbContext>, IDenpendencySingleton
    {
        private readonly IMediator _mediator;
        private readonly List<string> _dbConnectionStrings;

        public DbContextFactory(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;

            _dbConnectionStrings = GetAllDbInfo(configuration);
        }

        public ListenDbContext CreateDbContext()
        {
            int maxLength = _dbConnectionStrings.Count;
            // todo 需要添加读库是否可用
            if (maxLength == 0)
                throw new DomainException("至少需要一个可用的库");
            Random random = new Random();
            int indexNum = random.Next(maxLength);

            string connectionString = _dbConnectionStrings[indexNum];
            ListenDbContext dbContext = new ListenDbContext(_mediator, connectionString);

            return dbContext;
        }

        private List<string> GetAllDbInfo(IConfiguration configuration)
        {
            List<string> dbInfoList = new List<string>();
            string[] dbInfos = configuration.GetSection("DbConnection:SlaveDb").Get<string[]>();
            if (dbInfos == null)
            {
                string masterDbConnectionString = configuration.GetSection("DbConnection:MasterDb_Listen").Value;
                dbInfoList.Add(masterDbConnectionString);
            }
            else
            {
                dbInfoList = dbInfos.ToList();
            }
            return dbInfoList;
        }
    }
}