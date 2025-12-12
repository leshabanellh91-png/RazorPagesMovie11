using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize(Roles = "Admin")]
public class DeleteUserModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public DeleteUserModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public string UserId { get; set; }

    public IdentityUser UserToDelete { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return NotFound();

        UserToDelete = await _userManager.FindByIdAsync(id);
        if (UserToDelete == null) return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(UserId)) return NotFound();

        var user = await _userManager.FindByIdAsync(UserId);
        if (user == null) return NotFound();

        await _userManager.DeleteAsync(user);

        return RedirectToPage("/Admin/Users"); // back to users list
    }
}
