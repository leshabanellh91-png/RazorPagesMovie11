using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;

namespace RazorPagesMovie1.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RazorPagesMovie1Context(
                serviceProvider.GetRequiredService<DbContextOptions<RazorPagesMovie1Context>>()))
            {
                // Seed Directors first
                if (!context.Director.Any())
                {
                    context.Director.AddRange(
                        new Director { Name = "Rob Reiner" },
                        new Director { Name = "Christopher Nolan" },
                        new Director { Name = "Steven Spielberg" }
                    );

                    context.SaveChanges();
                }

                // Seed Movies next
                if (!context.Movie.Any())
                {
                    var robReiner = context.Director.First(d => d.Name == "Rob Reiner");
                    var nolan = context.Director.First(d => d.Name == "Christopher Nolan");

                    context.Movie.AddRange(
                        new Movie
                        {
                            Title = "When Harry Met Sally",
                            ReleaseDate = DateTime.Parse("1989-02-12"),
                            Genre = "Romantic Comedy",
                            Price = 7.99M,
                            DirectorId = robReiner.Id
                        },
                        new Movie
                        {
                            Title = "Inception",
                            ReleaseDate = DateTime.Parse("2010-07-16"),
                            Genre = "Sci-Fi",
                            Price = 9.99M,
                            DirectorId = nolan.Id
                        }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
