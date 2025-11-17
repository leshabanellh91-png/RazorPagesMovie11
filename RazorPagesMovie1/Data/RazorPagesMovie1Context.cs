using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Models;

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
        



    }
}
