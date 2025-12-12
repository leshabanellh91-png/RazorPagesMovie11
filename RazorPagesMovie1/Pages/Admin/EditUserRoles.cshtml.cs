using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize(Roles = "Admin")]
public class EditUserRolesModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public EditUserRolesModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [BindProperty]
    public string UserId { get; set; }

    [BindProperty]
    public List<string> SelectedRoles { get; set; } = new();

    public IdentityUser UserToEdit { get; set; }
    public List<string> AllRoles { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return NotFound();

        UserToEdit = await _userManager.FindByIdAsync(id);
        if (UserToEdit == null) return NotFound();

        AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
        SelectedRoles = (await _userManager.GetRolesAsync(UserToEdit)).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(UserId)) return NotFound();

        var user = await _userManager.FindByIdAsync(UserId);
        if (user == null) return NotFound();

        var currentRoles = await _userManager.GetRolesAsync(user);

        var rolesToAdd = SelectedRoles.Except(currentRoles);
        var rolesToRemove = currentRoles.Except(SelectedRoles);

        await _userManager.AddToRolesAsync(user, rolesToAdd);
        await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

        return RedirectToPage("/Admin/Users"); // back to users list
    }
}
