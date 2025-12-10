using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie1.Models
{
    public class Director
    {
        public int Id { get; set; }

        [Required, StringLength(60)]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
