using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
namespace WebApp1.Pages
{
[Authorize]
    public class ListRoomsModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        public IList<Room>? IRooms { get; set; }
        public Room? Room { get; set; } = new Room();

        public ListRoomsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            IRooms = await _context.Rooms.ToListAsync() ?? new List<Room>();
        }

        public async Task<IActionResult> OnPostRemove(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditRoomAsync(int roomId, string roomName, int capacity)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null)
            {
                return NotFound();
            }

            room.RoomName = roomName;
            room.Capacity = capacity;
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}