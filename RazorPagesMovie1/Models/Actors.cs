using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie1.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }

        [StringLength(100)]
        public string Nationality { get; set; } = string.Empty;
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }

    }
}
