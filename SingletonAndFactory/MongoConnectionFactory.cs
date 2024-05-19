namespace Singleton;

public class MongoConnectionFactory : IDataBaseConnectionFactory
{
    public DataBaseConnection Create(string connectionString)
    {
        return new MongoConnection(connectionString);
    }
}