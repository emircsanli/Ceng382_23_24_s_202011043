using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class RoomData
{
    [JsonPropertyName("Room")]
    public Room[] Rooms { get; set; }
}

public class Room
{
    [JsonPropertyName("roomId")]
    public string roomId { get; set; }

    [JsonPropertyName("roomName")]
    public string roomName { get; set; }

    [JsonPropertyName("capacity")]
    public int capacity { get; set; }
}

public class Reservation{
    
    public DateTime time;
    public DateTime date;
    public string reserverName;
    public Room room;
}

public class ReservationHandler{


}

class Program
{
    static void Main(string[] args)
    {
        int[] a=new int [2];
        a=[1,2];

for(int i=0;i<2;i++){
    Console.Write(a[i]);
}
        int[,]array = new int[2,2]{ { 1, 2 }, { 2, 3 } };

        //path to json into string
        //to do inside try catch
        string jsonFilePath = "Data.json";

        //options to read
        string jsonString = File.ReadAllText(jsonFilePath);

        var options = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
        };

        //read try catch
        var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options); //full string aldığımız datayı anlamlandıracak fonksiyon


        if (roomData?.Rooms != null)
        {
            //print
            foreach (var room in roomData.Rooms)
            {

                Console.WriteLine($"Room ID: {room.roomId}, Name: {room.roomName}, Capacity {room.capacity}");
            }

        }
        
        for(int i = 0 ; i<2 ; i++){

            for(int j = 0 ; j<2 ; j++){

                Console.Write(array[i,j]);
            }
            
            Console.WriteLine(" ");
        }

    }
}