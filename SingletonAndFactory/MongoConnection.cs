namespace Singleton;

public class MongoConnection(string connectionString) : DataBaseConnection
{
    public override void Connect()
    {
        Console.WriteLine($"Connected to MongoDB. Connection string is {connectionString}");
    }

    public override void Disconnect()
    {
        Console.WriteLine("Disconnected from MongoDB");

    }
}