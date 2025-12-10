using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;
using System;
using System.Linq;

namespace RazorPagesMovie1.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new RazorPagesMovie1Context(
                serviceProvider.GetRequiredService<DbContextOptions<RazorPagesMovie1Context>>());

            // Directors
            if (!context.Director.Any())
            {
                context.Director.AddRange(
                    new Director { Name = "Rob Reiner" },
                    new Director { Name = "Moriswi Simon" },
                    new Director { Name = "Lerato Lee" },
                    new Director { Name = "Lucy Mmasa" },
                    new Director { Name = "Mpho Nkuna" },
                    new Director { Name = "Mothiba Fortunate" }
                );
                context.SaveChanges();
            }

            // Actors
            if (!context.Actors.Any())
            {
                context.Actors.AddRange(
                    new Actor { FirstName = "Leonardo", LastName = "DiCaprio", BirthDate = new DateTime(1974, 11, 11), Nationality="American" },
                    new Actor { FirstName = "Samuel", LastName = "Jackson", BirthDate = new DateTime(1948, 12, 21), Nationality="American" },
                    new Actor { FirstName = "Meg", LastName = "Ryan", BirthDate = new DateTime(1961, 11, 19), Nationality="American" },
                    new Actor { FirstName = "Matthew", LastName = "McConaughey", BirthDate = new DateTime(1969, 11, 4), Nationality="American" },
                    new Actor { FirstName = "John", LastName = "Travolta", BirthDate = new DateTime(1954, 2, 18), Nationality="American" },
                    new Actor { FirstName = "Jamie", LastName = "Foxx", BirthDate = new DateTime(1967, 12, 13), Nationality="American" }
                );
                context.SaveChanges();
            }

            // Movies
            if (!context.Movie.Any())
            {
                var directors = context.Director.ToList();
                var actors = context.Actors.ToList();

                context.Movie.AddRange(
                    new Movie
                    {
                        Title="When Harry Met Sally",
                        ReleaseDate=new DateTime(1989, 2, 12),
                        Genre="Romantic Comedy",
                        Price=7.99M,
                        Rating="PG",
                        DirectorId=directors[0].Id,
                        ActorId=actors[2].Id,
                        ImageUrl="/images/sally3.jpg"
                    },
                    new Movie
                    {
                        Title="Inception",
                        ReleaseDate=new DateTime(2010, 7, 16),
                        Genre="Sci-Fi",
                        Price=9.99M,
                        Rating="PG",
                        DirectorId=directors[1].Id,
                        ActorId=actors[0].Id,
                        ImageUrl="/images/inception11.jpg"
                    },
                    new Movie
                    {
                        Title="E.T. the Extra-Terrestrial",
                        ReleaseDate=new DateTime(1982, 6, 11),
                        Genre="Science Fiction",
                        Price=7.99M,
                        Rating="PG",
                        DirectorId=directors[2].Id,
                        ActorId=actors[2].Id,
                        ImageUrl="/images/e.t 2.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
