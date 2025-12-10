using System;
using System.ComponentModel.DataAnnotations;
using RazorPagesMovie1.Models;

namespace RazorMovieProject.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }

        // Make navigation property OPTIONAL
        public Movie? Movie { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime ShowTime { get; set; }

        public string PaymentStatus { get; set; } = "Pending";

        [Required]
        [Range(1, 20)]
        public int NumberOfTickets { get; set; }

        [Required]
        public string SeatNumbers { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
    }
}
