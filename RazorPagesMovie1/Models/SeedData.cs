using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie1.Data;

namespace RazorPagesMovie.Models;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<RazorPagesMovie1Context>();
        var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();

        // Prevent duplicate seeding
        if (await context.Movie.AnyAsync() || await context.Director.AnyAsync())
        {
            logger.LogInformation("Database already seeded — skipping.");
            return;
        }

        logger.LogInformation("Seeding Directors...");

        var directors = new List<Director>
        {
            new Director { Name = "Rob Reiner", BirthDate = new DateTime(1947, 3, 6) },
            new Director { Name = "Ivan Reitman", BirthDate = new DateTime(1946, 10, 27) },
            new Director { Name = "Howard Hawks", BirthDate = new DateTime(1896, 5, 30) }
        };

        context.Director.AddRange(directors);
        await context.SaveChangesAsync();

        logger.LogInformation("Seeding Movies...");

        var movies = new List<Movie>
        {
            new Movie
            {
                Title = "When Harry Met Sally",
                ReleaseDate = new DateTime(1989, 2, 12),
                Genre = "Romantic Comedy",
                Price = 7.99M,
                Rating = "R",
                DirectorId = directors[0].Id
            },
            new Movie
            {
                Title = "Ghostbusters",
                ReleaseDate = new DateTime(1984, 3, 13),
                Genre = "Comedy",
                Price = 8.99M,
                Rating = "PG",
                DirectorId = directors[1].Id
            },
            new Movie
            {
                Title = "Ghostbusters 2",
                ReleaseDate = new DateTime(1986, 2, 23),
                Genre = "Comedy",
                Price = 9.99M,
                Rating = "PG",
                DirectorId = directors[1].Id
            },
            new Movie
            {
                Title = "Rio Bravo",
                ReleaseDate = new DateTime(1959, 4, 15),
                Genre = "Western",
                Price = 3.99M,
                Rating = "PG-13",
                DirectorId = directors[2].Id
            }
        };

        context.Movie.AddRange(movies);
        await context.SaveChangesAsync();

        logger.LogInformation("Seeding complete: {count} Movies, {directorCount} Directors",
            movies.Count, directors.Count);
    }
}
