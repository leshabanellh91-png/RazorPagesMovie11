using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie1.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie1Context _context;

        public IndexModel(RazorPagesMovie1Context context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        public SelectList Genre { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IList<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            // Query genres for dropdown
            IQueryable<string> genreQuery = _context.Movie
                .OrderBy(m => m.Genre)
                .Select(m => m.Genre)
                .Distinct();

            // Load movies including Actor and Director
            var movies = _context.Movie
                .Include(m => m.Actor)
                .Include(m => m.Director)
                .AsQueryable();

            // Filter by title
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(m => m.Title.Contains(SearchString));
            }

            // Filter by genre
            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(m => m.Genre == MovieGenre);
            }

            Genre = new SelectList(await genreQuery.ToListAsync());
            Movies = await movies.ToListAsync();
        }
    }
}
