using Microsoft.EntityFrameworkCore;
using RazorMovieProject.Models;
using RazorPagesMovie.Models;

namespace RazorPagesMovie1.Data
{
    public class RazorPagesMovie1Context : DbContext
    {
        public RazorPagesMovie1Context (DbContextOptions<RazorPagesMovie1Context> options)
            : base(options)
        {
        }

        public DbSet<Director> Director { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public object Booking { get; internal set; }
    }
}
