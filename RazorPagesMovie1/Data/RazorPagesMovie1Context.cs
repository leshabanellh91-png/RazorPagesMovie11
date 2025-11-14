using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie1.Data
{
    public class RazorPagesMovie1Context : DbContext
    {
        public RazorPagesMovie1Context(DbContextOptions<RazorPagesMovie1Context> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesMovie.Models.Movie> Movies { get; set; } = default!;
        public DbSet<Director> Director { get; set; } = default!;
        public DbSet<Actor> Actors { get; set; } = default!; 
    }
}
