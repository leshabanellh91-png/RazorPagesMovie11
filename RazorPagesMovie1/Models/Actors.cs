using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie1.Models
{
    public class Actors
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [StringLength(100)]
        public string Nationality { get; set; }

    }
}
