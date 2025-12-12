using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageMovies.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<UserWithRoles> Users { get; set; } = new List<UserWithRoles>();

        public class UserWithRoles
        {
            public IdentityUser User { get; set; }
            public IList<string> Roles { get; set; }
        }

        public async Task OnGetAsync()
        {
            Users = new List<UserWithRoles>();

            foreach (var user in _userManager.Users.ToList())
            {
                var roles = await _userManager.GetRolesAsync(user);
                Users.Add(new UserWithRoles
                {
                    User = user,
                    Roles = roles
                });
            }
        }
    }
}
