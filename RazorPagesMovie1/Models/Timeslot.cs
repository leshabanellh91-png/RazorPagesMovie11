using RazorMovieProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class Timeslot
    {
        public int Id { get; set; }

        [Required]
        public DateTime ShowTime { get; set; }

        public int MovieId { get; set; }
        public required Movie Movie { get; set; }

        public required ICollection<Booking> Bookings { get; set; }
    }
}
