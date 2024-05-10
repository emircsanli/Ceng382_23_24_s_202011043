using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using ReservationSystem;

public class FileLogger : ILogger
{
    public static List<LogRecord>logList;
    private readonly string FilePath = "datas/LogData.json";

    public void log(LogRecord record){
        string jsonLog = $"{{\"Record Timestamp\": \"{record.timeStamp}\", \"Record Message\": \"{record.message}\"}}";

       try
        {
            if (logList == null)
            {
                logList = new List<LogRecord>();
            }

            logList.Add(record);

    }
    catch (Exception ex)
        {
            Console.WriteLine("Error saving Log ! " + ex.Message);
        }
        File.AppendAllText(FilePath, jsonLog + Environment.NewLine);
        string json = "{\"Loggers\": " + JsonSerializer.Serialize(record) + "}";
            File.WriteAllText(FilePath, json);
    
    }
    public void logMessage(string message)
    {
         Console.WriteLine(message);
    }
}
