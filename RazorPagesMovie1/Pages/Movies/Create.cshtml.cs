using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;
using System.IO;
using System.Threading.Tasks;

namespace RazorPagesMovie1.Pages.Movies
{
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
        public IFormFile MovieImage { get; set; } // uploaded file

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

                Movie.ImageUrl = "/images/" + fileName; // save relative path
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
