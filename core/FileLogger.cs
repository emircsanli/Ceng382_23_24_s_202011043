using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using ReservationSystem;


public class FileLogger : ILogger
{
    private readonly string FilePath = "datas/LogData.json";
    

    public void LogRecord(LogRecord log)
    {
        string jsonLog = $"{{\"Timestamp\": \"{log.TimeStamp}\", \"Reserver Name\": \"{log.ReserverName}\", \"Room Name\": \"{log.RoomName}\"}}";

        File.AppendAllText(FilePath, jsonLog + Environment.NewLine);
        string json = "{\"Loggers\": " + JsonSerializer.Serialize(LogRecord) + "}";
            File.WriteAllText(FilePath, json);
    }

}
