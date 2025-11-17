using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;

namespace RazorPagesMovie1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie1.Data.RazorPagesMovie1Context _context;

        public IndexModel(RazorPagesMovie1.Data.RazorPagesMovie1Context context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie
                .Include(m => m.Actor)
                .Include(m => m.Director).ToListAsync();
        }
    }
}
