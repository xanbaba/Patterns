namespace Singleton;

public interface IDataBaseConnectionFactory
{
    public DataBaseConnection Create(string connectionString);
}