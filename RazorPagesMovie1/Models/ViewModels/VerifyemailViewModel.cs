using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie1.Models.ViewModels
{
    public class VerifyemailViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]

        public required string Email { get; set; }
    }
}
