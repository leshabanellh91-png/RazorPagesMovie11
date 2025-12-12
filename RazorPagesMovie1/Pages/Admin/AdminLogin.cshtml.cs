using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AdminLoginModel : PageModel
{
    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    public IActionResult OnPost()
    {
        // Example: hardcoded admin credentials
        if (Username == "adminuser" && Password == "admin@movies.com")
        {
            // redirect to admin dashboard
            return RedirectToPage("/AdminDashboard");
        }
        else
        {
            ErrorMessage = "Invalid username or password";
            return Page();
        }
    }
}

