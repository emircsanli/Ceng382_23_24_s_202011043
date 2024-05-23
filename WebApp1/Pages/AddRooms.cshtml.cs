using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Data;

namespace WebApp1.Pages
{
[Authorize]

public class AddRoomsModel : PageModel
{


    private readonly ApplicationDbContext _context;

    public AddRoomsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Room? Room { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {

        if (ModelState.IsValid)
        {
            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();
            Console.WriteLine(Room.Id + " " + Room.RoomName + " " + Room.Capacity + " has been added successfully!");
            return RedirectToPage("./ListRooms", new { success = true });
        }
        else
        {
            Console.WriteLine("Room can not add to DB!");
            return RedirectToPage("./Index", new { success = false });
        }
    }

}
}