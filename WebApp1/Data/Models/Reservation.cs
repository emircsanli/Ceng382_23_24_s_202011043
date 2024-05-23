public class Reservation{
    
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public DateTime Date { get; set; }
    public string? ReserverName { get; set; }
    public Room? Room {get; set; }
}