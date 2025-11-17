using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Models;

namespace RazorPagesMovie1.Pages.Movies
{
    public class IndexModel : PageModel
    {
        // Selected genre from the query string
        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        // SelectList used by the <select> in the Razor page
        public SelectList Genre { get; set; }

        // Search string from the query string
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        // Movies displayed in the page
        public IList<Movie> Movies { get; set; } = new List<Movie>();

        // Note: This file only adds the missing properties required by the Razor page.
        // If you want the Genre SelectList and Movies list populated automatically,
        // add a constructor injecting your DbContext and implement OnGet to fill them.
    }
}