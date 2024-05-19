using System.Text.Json;

namespace Singleton;

public class JsonConfigReaderSingleton
{
    private static JsonConfigReaderSingleton? _instance;

    public static JsonConfigReaderSingleton Instance => _instance ??= new JsonConfigReaderSingleton();
    
    public static string? FileName { get; set; }
    
    public string Read(string key)
    {
        if (FileName == null)
        {
            throw new InvalidOperationException($"Initialize {nameof(FileName)} property before reading the config");
        }

        var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(FileName));
        if (config == null)
        {
            throw new ArgumentException("Error occured while parsing the config");
        }
        return config[key];
    }
}