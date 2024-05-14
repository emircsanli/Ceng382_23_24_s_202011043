
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace MyApp.Namespace
{

    public class AddRoomsModel : PageModel
    {


        private readonly AppDbContext _context; 

        public AddRoomsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Room ? Room { get; set; }
      
        public async Task<IActionResult> OnPostAsync()
        {
            
                if (ModelState.IsValid)
            {
                _context.Rooms.Add(Room);
                await _context.SaveChangesAsync();
                Console.WriteLine(Room.Id + " " + Room.RoomName + " " + Room.Capacity + " has been added successfully!");
                return RedirectToPage("./ListRooms", new { success = true });
            }
            else{
                Console.WriteLine("Room can not add to DB!");
                return RedirectToPage("./Index",  new { success = false });
            }
        }
            
        }
    }

