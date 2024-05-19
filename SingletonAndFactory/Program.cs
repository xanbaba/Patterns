// See https://aka.ms/new-console-template for more information

using Singleton;

SingletonFileLogger.FileName = "log.txt";
SingletonFileLogger.Instance.Log(LogType.Error, "Error nigga");
SingletonFileLogger.Instance.Log(LogType.Info, "Info nigga");

JsonConfigReaderSingleton.FileName = "config.json";
Console.WriteLine(JsonConfigReaderSingleton.Instance.Read("Key"));