using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorMovieProject.Models;
using RazorPagesMovie1.Data;

namespace RazorPagesMovie1.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovie1Context _context;

        public CreateModel(RazorPagesMovie1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; } = new Booking
        {
            Movie = new RazorPagesMovie1.Models.Movie(), // Placeholder, will be set by model binding or manually
            UserId = string.Empty, // Placeholder, should be set before saving
            SeatNumbers = string.Empty // Placeholder, should be set before saving
        };

        // GET request
        public IActionResult OnGet(int? movieId)
        {
            // populate dropdown
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title");

            if (movieId.HasValue)
            {
                Booking.MovieId = movieId.Value;
            }

            return Page();
        }

        // POST request
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Booking failed. Please check your details.";
                TempData["MessageType"] = "error";
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Your booking was successful!";
            TempData["MessageType"] = "success";

            return RedirectToPage("./Index");
        }

    }
}
