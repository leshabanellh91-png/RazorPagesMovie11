using RazorMovieProject.Models;
using RazorPagesMovie.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RazorMovieProject.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public string UserId { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime ShowTime { get; set; }

        [Required]
        [Range(1, 20)]
        public int NumberOfTickets { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
    }
}
