namespace Singleton;

public class MsSqlConnectionFactory : IDataBaseConnectionFactory
{
    public DataBaseConnection Create(string connectionString)
    {
        return new MsSqlConnection(connectionString);
    }
}