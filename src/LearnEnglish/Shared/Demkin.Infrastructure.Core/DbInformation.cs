namespace Demkin.Infrastructure.Core
{
    public class DbInformation
    {
        public string ContextName { get; set; }

        public string ConnectionString { get; set; }

        public int HitRate { get; set; }

        public DataBaseType DataBaseType { get; set; }
    }

    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4,
    }
}