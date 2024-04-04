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

public class Reservation
{
    public DateTime time { get; set; }
    public DateTime date { get; set; }
    public string reserverName { get; set; }
    public Room room { get; set; }
}

public class ReservationHandler
{
    public Reservation[,] reservations { get; set; } = new Reservation[10, 7];


    public void addReservation(Reservation reservation)
    {

        bool reservationAdded = false;

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (reservations[j, i] == null)
                {
                    reservations[j, i] = reservation;
                    reservationAdded = true;
                    break;
                }
            }
            if (reservationAdded)
                break;
        }

        if (reservationAdded)
        {
            Console.WriteLine("Your reservation successfully added: " + reservation.reserverName + " to " + reservation.room.roomId);
        }
        else
        {
            Console.WriteLine("Weekly schedule is full.");
        }
    }

    public void deleteReservation(Reservation reservation)
    {
        bool reservationDeleted = false;

        for (int row = 0; row < 10; row++)
        {
            for (int column = 0; column < 7; column++)
            {
                if (reservations[row, column] != null && reservation == reservations[row, column])
                {
                    reservations[row, column] = null;
                    reservationDeleted = true;
                }
            }
        }

        if (reservationDeleted)
        {
            Console.WriteLine(reservation.room.roomId + "-" +reservation.reserverName + "'s reservation deleted.");
        }
        else
        {
            Console.WriteLine("Reservation not found.");
        }
    }

    public void displayWeeklySchedule()
    {
        Console.WriteLine("Weekly Reservation Schedule:");
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------");
        Console.Write("Time\t");

        string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        foreach (var day in daysOfWeek)
        {
            Console.Write(day.PadRight(20));
        }
        Console.WriteLine();
        Console.Write("----------------------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine();

        for (int i = 0; i < 10; i++)
        {
            Console.Write($"{(i + 9).ToString("00")}:00|\t");
            for (int j = 0; j < 7; j++)
            {
                string savedReservation = reservations[i, j]?.reserverName ?? " ";
                Console.Write(savedReservation.PadRight(20));
            }
            Console.WriteLine();
        }
        Console.Write("----------------------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
    }
}

class Program
{
    static void Main(String[] args)
    {

        //define file path
        string filePath = "Data.json";

        //Read from json
        // 1 -> json to text
        string jsonString = File.ReadAllText(filePath);
        // 2 -> decode text into meaningful classes
        var roomData = JsonSerializer.Deserialize<RoomData>(
                                jsonString,
                                new JsonSerializerOptions()
                                {
                                    NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
                                });


        //print
        if (roomData?.Rooms != null)
        {
            foreach (var room in roomData.Rooms)
            {
                Console.WriteLine($"Room ID : {room.roomId} RoomName : {room.roomName} Capacity : {room.capacity}");
            }
        }
        ReservationHandler reservationHandler = new ReservationHandler();

        Reservation[] reservationsArray = new Reservation[70];
        Random random = new Random();

        for (int i = 0; i < reservationsArray.Length; i++)
        {
            Reservation newReservation = new Reservation();
            newReservation.time = DateTime.Now;
            newReservation.date = DateTime.Today;
            newReservation.reserverName = $"Customer-{i + 1}";
            newReservation.room = roomData.Rooms[random.Next(roomData.Rooms.Length)];

            reservationsArray[i] = newReservation;
            reservationHandler.addReservation(newReservation);
        }
        Console.WriteLine("Rezervations added randomly rooms)");
        Console.WriteLine();

        reservationHandler.displayWeeklySchedule();

       for (int i = 0; i < 5; i++)
{
    int randomIndex = random.Next(reservationsArray.Length);
    Reservation reservationToDelete = reservationsArray[randomIndex];

    if ( reservationsArray[randomIndex]!= null)
    {
        reservationHandler.deleteReservation(reservationToDelete);
        reservationsArray[randomIndex] = null; 
    }
    else
    {
        Console.WriteLine("Previously deleted.");
        
    }
}

Console.WriteLine("5 random reservations deleted");
Console.WriteLine();

reservationHandler.displayWeeklySchedule();

     

    }
}