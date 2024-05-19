namespace Singleton;

public sealed class SingletonFileLogger
{
    private static SingletonFileLogger? _instance;

    public static SingletonFileLogger Instance => _instance ??= new SingletonFileLogger();
    
    public static string? FileName { get; set; }

    private SingletonFileLogger()
    {
        
    }

    public void Log(LogType logType, string message)
    {
        if (FileName == null)
        {
            throw new InvalidOperationException($"Initialize {nameof(FileName)} property before logging");
        }
        File.AppendAllText(FileName, GenerateMessage(logType, message));
    }

    private string GenerateMessage(LogType logType, string message)
    {
        return $"[{logType.ToString()}] {message};\n";
    }
}