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

        // Selected genre from the query string
        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        // SelectList used by the <select>
        public SelectList Genre { get; set; }

        // Search string from the query string
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        // Movies displayed on the page
        public IList<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            // Query genres
            IQueryable<string> genreQuery = _context.Movie
                .OrderBy(m => m.Genre)
                .Select(m => m.Genre)
                .Distinct();

            // Base query
            var movies = _context.Movie.AsQueryable();

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

            // Load results into model
            Genre = new SelectList(await genreQuery.ToListAsync());
            Movies = await movies.ToListAsync();
        }
    }
}
