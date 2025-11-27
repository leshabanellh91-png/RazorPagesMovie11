using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;

namespace RazorPagesMovie1.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]

        public required string Email { get; set; }

    }
}
