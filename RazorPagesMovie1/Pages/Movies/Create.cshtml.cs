using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;

namespace RazorPagesMovie1.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovie1Context _context;

        public CreateModel(RazorPagesMovie1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        // Dropdown lists for Director and Actor
        public SelectList DirectorList { get; set; } = default!;
        public SelectList ActorList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            DirectorList = new SelectList(await _context.Director.ToListAsync(), "Id", "Name");
            ActorList = new SelectList(await _context.Actors.ToListAsync(), "Id", "FirstName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                DirectorList = new SelectList(await _context.Director.ToListAsync(), "Id", "Name");
                ActorList = new SelectList(await _context.Actors.ToListAsync(), "Id", "FirstName");
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
