using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie1.Models.ViewModels
{
    public class LoginView
    {
        // If Myproperty isn't used, remove it.
        // public string Myproperty { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(40, MinimumLength = 8,
            ErrorMessage = "The {0} must be at least {2} and at most {1} characters.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        // If you are working with login, you might not need ConfirmPassword
        // If it's a registration view, then you keep this.
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
