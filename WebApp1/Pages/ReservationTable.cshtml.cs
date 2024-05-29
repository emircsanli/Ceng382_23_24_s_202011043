using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp1.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace WebApp1.Pages
{
    [Authorize]
    public class ReservationTableModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<Reservation> IReservations { get; set; }
        public List<string> DaysOfWeek { get; set; }
        public List<string> HoursOfDay { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RoomName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Capacity { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(6 - (int)DateTime.Today.DayOfWeek);

        public ReservationTableModel(ApplicationDbContext context)
        {
            _context = context;
            DaysOfWeek = new List<string> { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            HoursOfDay = Enumerable.Range(0, 24).Select(i => $"{i:00}:00").ToList();
        }

        public async Task OnGetAsync()
        {
            var query = _context.Reservations
                                .Include(r => r.Room)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(RoomName))
            {
                query = query.Where(r => r.Room.RoomName.Contains(RoomName));
            }

            if (Capacity > 0)
            {
                query = query.Where(r => r.Room.Capacity == Capacity);
            }

            if (StartDate != DateTime.MinValue && EndDate != DateTime.MinValue)
            {
                query = query.Where(r => r.Date >= StartDate && r.Date <= EndDate);
            }

            IReservations = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int id, string roomName, DateTime date, DateTime time)
        {
            var reservation = await _context.Reservations
                                            .Include(r => r.Room)
                                            .FirstOrDefaultAsync(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.Room == null)
            {
                reservation.Room = new Room();
            }

            reservation.Room.RoomName = roomName;
            reservation.Date = date;
            reservation.Time = time;
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
