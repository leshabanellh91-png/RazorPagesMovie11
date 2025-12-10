using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie1.Models;
using RazorPagesMovie1.Data;

namespace RazorPagesMovie.Pages.Movies
{
    public class UpcomingModel : PageModel
    {
        private readonly RazorPagesMovie1Context _context;

        public UpcomingModel(RazorPagesMovie1Context context)
        {
            _context = context;
        }

        public List<Movie> UpcomingMovies { get; set; }

        public async Task OnGetAsync()
        {
            // Get movies that release in the future
            UpcomingMovies = await _context.Movie
                .Where(m => m.ReleaseDate > DateTime.Now)
                .OrderBy(m => m.ReleaseDate)
                .ToListAsync();
        }
    }
}
