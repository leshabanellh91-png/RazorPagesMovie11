using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public required string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public required DateTime BirthDate { get; set; }

        [StringLength(100)]
        public required string Nationality { get; set; }

    }
}
