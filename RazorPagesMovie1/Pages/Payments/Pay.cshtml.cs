using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorMovieProject.Models;
using RazorPagesMovie1.Data;

namespace RazorPagesMovie.Pages.Payments
{
    public class PayModel : PageModel
    {
        private readonly RazorPagesMovie1Context _context;

        public PayModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int bookingId)
        {
            Booking = await _context.Bookings
                                     .Include(b => b.Movie)
                                     .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (Booking == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            booking.PaymentStatus = "Paid";

            await _context.SaveChangesAsync();

            return RedirectToPage("/Bookings/Confirmation", new { id = booking.Id });
        }
    }
}
