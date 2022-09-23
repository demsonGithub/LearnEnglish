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

        public MyDbContext CreateDbContext(ReadAndWrite readAndWrite = ReadAndWrite.Write)
        {
            switch (readAndWrite)
            {
                case ReadAndWrite.Write:
                    return _listenDbContext;

                case ReadAndWrite.Read:
                    return _dbContext2;

                default:
                    return _listenDbContext;
            }
        }
    }
}