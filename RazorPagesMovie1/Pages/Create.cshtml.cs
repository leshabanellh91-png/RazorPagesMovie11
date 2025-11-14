using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;

namespace RazorPagesMovie1.Pages
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovie1.Data.RazorPagesMovie1Context _context;

        public CreateModel(RazorPagesMovie1.Data.RazorPagesMovie1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "FirstName");
        ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
