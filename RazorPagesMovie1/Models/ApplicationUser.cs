using Microsoft.AspNetCore.Identity;

namespace RazorPagesMovie1.Models
{
    public class User : IdentityUser
    {
        public string FullName
        {
            get; set;
        }
    }
}


