using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;

namespace RazorPagesMovie1.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie1Context _context;

        public IndexModel(RazorPagesMovie1Context context)
        {
            _context = context;
        }

        // List of movies to display
        public IList<Movie> Movies { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Include Director and Actor navigation properties
            Movies = await _context.Movie
                .Include(m => m.Director)
                .Include(m => m.Actor)
                .ToListAsync();
        }
    }
}
