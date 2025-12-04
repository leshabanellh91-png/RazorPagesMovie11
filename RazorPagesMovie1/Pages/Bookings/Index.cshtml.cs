using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorMovieProject.Models;
using RazorPagesMovie1.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace RazorPagesMovie1.Pages.Bookings
{

    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie1Context _context;

        public IndexModel(RazorPagesMovie1Context context)
        {
            _context = context;
        }

        public IList<Booking> Bookings { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Bookings = await _context.Bookings
                            .Include(b => b.Movie) // <-- Important: Include related Movie entity
                            .ToListAsync();
        }

    }
}
