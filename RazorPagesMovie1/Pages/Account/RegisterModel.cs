using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie1.Models.ViewModels;

namespace RazorPagesMovie1.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel? RegisterViewModel { get; set; }

        public void OnGet()
        {
            // Handle GET request (if any initialization is required).
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Perform registration logic, such as creating a user.
                return RedirectToPage("/Account/Login"); // Redirect to login page after successful registration.
            }

            return Page(); // Return to the same page with validation errors.
        }
    }
}
