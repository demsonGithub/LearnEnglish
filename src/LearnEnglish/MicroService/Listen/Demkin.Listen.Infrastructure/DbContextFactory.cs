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
            _dbConnectionStrings = GetAllDbInfo();
            _mediator = mediator;
        }

        public ListenDbContext CreateDbContext()
        {
            if (_dbConnectionStrings.Count == 0)
                throw new DomainException("至少需要一个可用的从库");
            Random random = new Random();
            int indexNum = random.Next(_dbConnectionStrings.Count);

            string connectionString = _dbConnectionStrings[indexNum];
            ListenDbContext dbContext = new ListenDbContext(_mediator, connectionString);

            return dbContext;
        }

        private List<string> GetAllDbInfo()
        {
            List<string> dbInfos = new List<string>();
            string test = "server=192.168.1.7;uid=sa;pwd=abc123#;database=LearnEnglish_Listen11;";
            dbInfos.Add(test);
            return dbInfos;
        }
    }
}