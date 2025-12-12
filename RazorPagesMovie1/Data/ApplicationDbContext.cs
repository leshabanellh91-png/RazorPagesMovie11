using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie1.Models;

namespace RazorPagesMovie1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<Actor> Actors { get; set; }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // VERY IMPORTANT: Prevent EF from mapping Movie-related entities
            foreach (var entity in builder.Model.GetEntityTypes().ToList())
            {
                if (entity.ClrType.Namespace != "Microsoft.AspNetCore.Identity")
                {
                  

                }
            }
        }
    }
}
