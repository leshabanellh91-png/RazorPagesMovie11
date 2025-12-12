using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace RazorPageMovies.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
