namespace Singleton;

public class SqlLiteConnection(string connectionString) : DataBaseConnection
{
    public override void Connect()
    {
        Console.WriteLine($"Connected to SqlLite. Connection string is {connectionString}");
    }

    public override void Disconnect()
    {
        Console.WriteLine("Disconnected from SqlLite");

    }
}