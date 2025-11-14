using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models;

public class Director
{
    public int Id { get; set; }

    [Required]
    [StringLength(60)]
    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
}
