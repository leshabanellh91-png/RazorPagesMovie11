using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;
using System;
using System.IO;
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
        public Movie Movie { get; set; }

        [BindProperty]
        public IFormFile MovieImage { get; set; }

        [BindProperty]
        public IFormFile TrailerFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Movie = await _context.Movie.FindAsync(id);

            if (Movie == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var movieToUpdate = await _context.Movie.FindAsync(Movie.Id);
            if (movieToUpdate == null)
                return NotFound();

            // Update simple fields
            movieToUpdate.Title = Movie.Title;
            movieToUpdate.ReleaseDate = Movie.ReleaseDate;
            movieToUpdate.Genre = Movie.Genre;
            movieToUpdate.Price = Movie.Price;
            movieToUpdate.Rating = Movie.Rating;
            movieToUpdate.DirectorId = Movie.DirectorId;
            movieToUpdate.ActorId = Movie.ActorId;

            // Update poster file
            if (MovieImage != null)
            {
                var folder = Path.Combine(_environment.WebRootPath, "images");
                Directory.CreateDirectory(folder);

                string uniqueName = Guid.NewGuid() + Path.GetExtension(MovieImage.FileName);
                string path = Path.Combine(folder, uniqueName);

                using var fs = new FileStream(path, FileMode.Create);
                await MovieImage.CopyToAsync(fs);

                movieToUpdate.ImageUrl = "/images/" + uniqueName;
            }

            // Update trailer file
            if (TrailerFile != null)
            {
                var folder = Path.Combine(_environment.WebRootPath, "uploads", "trailers");
                Directory.CreateDirectory(folder);

                string uniqueName = Guid.NewGuid() + Path.GetExtension(TrailerFile.FileName);
                string path = Path.Combine(folder, uniqueName);

                using var fs = new FileStream(path, FileMode.Create);
                await TrailerFile.CopyToAsync(fs);

                movieToUpdate.Trail = "/uploads/trailers/" + uniqueName;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
