using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesMovie1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // VERY IMPORTANT: Prevent EF from mapping Movie-related entities
            foreach (var entity in builder.Model.GetEntityTypes().ToList())
            {
                if (entity.ClrType.Namespace != "Microsoft.AspNetCore.Identity")
                {
                    builder.Model.RemoveEntityType(entity);
                }
            }
        }
    }
}
