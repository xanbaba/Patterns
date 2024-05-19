namespace Singleton;

public class SqlLiteConnectionFactory : IDataBaseConnectionFactory
{
    public DataBaseConnection Create(string connectionString)
    {
        return new SqlLiteConnection(connectionString);
    }
}