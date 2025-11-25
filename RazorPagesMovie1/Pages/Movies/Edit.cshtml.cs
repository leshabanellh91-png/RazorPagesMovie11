using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie1.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesMovie1Context _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(RazorPagesMovie1Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty]
        public IFormFile MovieImage { get; set; } // new poster

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (Movie == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var movieToUpdate = await _context.Movie.FindAsync(Movie.Id);
            if (movieToUpdate == null) return NotFound();

            // Update basic fields
            movieToUpdate.Title = Movie.Title;
            movieToUpdate.Genre = Movie.Genre;
            movieToUpdate.ReleaseDate = Movie.ReleaseDate;
            movieToUpdate.Price = Movie.Price;
            movieToUpdate.Rating = Movie.Rating;
            movieToUpdate.DirectorId = Movie.DirectorId;
            movieToUpdate.ActorId = Movie.ActorId;

            // Handle image upload
            if (MovieImage != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);
                var fileName = Path.GetFileName(MovieImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await MovieImage.CopyToAsync(fileStream);
                }

                movieToUpdate.ImageUrl = "/images/" + fileName;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.Id)) return NotFound();
                else throw;
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
