using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;

namespace RazorPagesMovie1.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesMovie1Context _context;

        public EditModel(RazorPagesMovie1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public SelectList DirectorList { get; set; } = default!;
        public SelectList ActorList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (Movie == null) return NotFound();

            DirectorList = new SelectList(await _context.Director.ToListAsync(), "Id", "Name", Movie.DirectorId);
            ActorList = new SelectList(await _context.Actors.ToListAsync(), "Id", "FirstName", Movie.ActorId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                DirectorList = new SelectList(await _context.Director.ToListAsync(), "Id", "Name", Movie.DirectorId);
                ActorList = new SelectList(await _context.Actors.ToListAsync(), "Id", "FirstName", Movie.ActorId);
                return Page();
            }

            _context.Attach(Movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
