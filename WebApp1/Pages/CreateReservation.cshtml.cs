using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp1.Data;

namespace WebApp1.Pages
{
    [Authorize]
    public class CreateReservationModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        public IList<Room> Rooms { get; set; }
        public Room? Room { get; set; } = new Room();

         [BindProperty]
        public Reservation Reservation { get; set; } = new Reservation();

        public CreateReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Rooms = await _context.Rooms.ToListAsync();
        }


        public async Task<IActionResult> OnPostAsync(int roomId)
        {

            if (ModelState.IsValid)
            {
                var room = await _context.Rooms.FindAsync(roomId);
                if (room == null)
                {
                    return NotFound();
                }

                Reservation.Room = room;

                _context.Reservations.Add(Reservation);
                await _context.SaveChangesAsync();
                Console.WriteLine(Reservation.Id + "id number, " + Reservation.ReserverName + "for this name, " + Reservation.Room + " for this room, has been added successfully!");
                return RedirectToPage("./ReservationTable");
            }
            else
            {
                Console.WriteLine("Reservation can not created!");
                return RedirectToPage("./HomePage");
            }
        }
    }
}