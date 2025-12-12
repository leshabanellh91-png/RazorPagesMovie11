using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPageMovies.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class SettingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SettingsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<SiteSetting> Settings { get; set; } = new List<SiteSetting>();

        public async Task OnGetAsync()
        {
            Settings = await _context.SiteSettings.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var setting in Settings)
            {
                var existing = await _context.SiteSettings.FirstOrDefaultAsync(s => s.Id == setting.Id);
                if (existing != null)
                {
                    existing.Value = setting.Value;
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Settings updated successfully!";
            return RedirectToPage();
        }
    }
}
