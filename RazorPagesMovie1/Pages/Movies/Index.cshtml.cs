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

        // Filters
        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        // Select list for genres
        public SelectList Genre { get; set; }

        // Movies list
        public IList<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            // Query distinct genres for dropdown
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = _context.Movie
                                 .Include(m => m.Actor)
                                 .Include(m => m.Director)
                                 .AsQueryable();

            // Filter by search string
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(m => m.Title.Contains(SearchString));
            }

            // Filter by genre
            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(m => m.Genre == MovieGenre);
            }

            Genre = new SelectList(await genreQuery.Distinct().ToListAsync());

            Movies = await movies.ToListAsync();


        }
        public async Task<IActionResult> OnPostRandomPickAsync()
        {
            // Get all movie IDs
            var movieIds = await _context.Movie.Select(m => m.Id).ToListAsync();

            if (!movieIds.Any())
                return Page(); // No movies available

            // Pick a random movie
            var random = new Random();
            int randomId = movieIds[random.Next(movieIds.Count)];

            // Redirect to Details page for the selected movie
            return RedirectToPage("/Movies/Details", new { id = randomId });
        }

    }
}
