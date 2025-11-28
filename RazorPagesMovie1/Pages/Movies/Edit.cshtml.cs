using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;

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
        public Movie Movie { get; set; }

        [BindProperty]
        public IFormFile MovieImage { get; set; }

        [BindProperty]
        public IFormFile TrailerFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Movie = await _context.Movie.FindAsync(id);
            if (Movie == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var movieToUpdate = await _context.Movie.FindAsync(Movie.Id);
            if (movieToUpdate == null)
                return NotFound();

            // Update text fields
            movieToUpdate.Title = Movie.Title;
            movieToUpdate.ReleaseDate = Movie.ReleaseDate;
            movieToUpdate.Genre = Movie.Genre;
            movieToUpdate.Price = Movie.Price;
            movieToUpdate.Rating = Movie.Rating;

            // Update image if uploaded
            if (MovieImage != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(MovieImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await MovieImage.CopyToAsync(fileStream);

                movieToUpdate.ImageUrl = "/images/" + uniqueFileName;
            }

            // Update trailer if uploaded
            if (TrailerFile != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "trailers");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(TrailerFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await TrailerFile.CopyToAsync(fileStream);

                movieToUpdate.Trail = "/uploads/trailers/" + uniqueFileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
