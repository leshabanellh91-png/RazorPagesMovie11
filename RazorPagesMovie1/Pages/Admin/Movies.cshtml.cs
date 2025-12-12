using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;

namespace RazorPageMovies.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class MoviesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MoviesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            Movies = await _context.Movies.ToListAsync();
        }
    }
}
