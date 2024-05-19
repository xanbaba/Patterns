namespace Singleton;

public class MsSqlConnection(string connectionString) : DataBaseConnection
{
    public override void Connect()
    {
        Console.WriteLine($"Connected to MsSql. Connection string is {connectionString}");
    }

    public override void Disconnect()
    {
        Console.WriteLine("Disconnected from MsSql");

    }
}