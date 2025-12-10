using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie1.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;



namespace RazorPagesMovie1.Pages.Movies
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovie1Context _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(RazorPagesMovie1Context context, IWebHostEnvironment environment)
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

        public SelectList DirectorList { get; set; }
        public SelectList ActorList { get; set; }

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

            // -------------------------------
            // ✅ STEP 3: SAVE MOVIE POSTER IMAGE
            // -------------------------------
            if (MovieImage != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(MovieImage.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await MovieImage.CopyToAsync(stream);

                // Save URL
                Movie.ImageUrl = "/images/" + uniqueFileName;
            }

            // -------------------------------
            // 🎬 SAVE TRAILER (video file)
            // -------------------------------
            if (TrailerFile != null)
            {
                var trailerFolder = Path.Combine(_environment.WebRootPath, "trailers");
                Directory.CreateDirectory(trailerFolder);

                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(TrailerFile.FileName);
                var filePath = Path.Combine(trailerFolder, uniqueFileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await TrailerFile.CopyToAsync(stream);

                // Save trailer URL (correct property)
                Movie.Trail = "/trail/" + uniqueFileName;
            }

            // SAVE MOVIE
            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Movies/Index");
        }
    }
}
