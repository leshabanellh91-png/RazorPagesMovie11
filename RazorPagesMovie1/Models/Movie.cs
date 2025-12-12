using RazorMovieProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie1.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, StringLength(60, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required, StringLength(30)]
        public string Genre { get; set; } = string.Empty;

        [Required, StringLength(5)]
        public string Rating { get; set; } = string.Empty;

        public int DirectorId { get; set; }
        public Director? Director { get; set; }

        public int ActorId { get; set; }
        public Actor? Actor { get; set; }

        public string? ImageUrl { get; set; }
        public string? Trail { get; set; }
        public bool Isfavorite { get; set; }
        public int Year { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
