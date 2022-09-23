namespace Demkin.Listen.Infrastructure
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly ListenDbContext _listenDbContext;
        private readonly ListenDbContext2 _dbContext2;

        public DbContextFactory(ListenDbContext listenDbContext, ListenDbContext2 dbContext2)
        {
            _listenDbContext = listenDbContext;
            _dbContext2 = dbContext2;
        }

        public MyDbContext CreateMasterDbContext()
        {
            return _listenDbContext;
        }

        public MyDbContext CreateSlaveDbContext()
        {
            return _dbContext2;
        }
    }
}