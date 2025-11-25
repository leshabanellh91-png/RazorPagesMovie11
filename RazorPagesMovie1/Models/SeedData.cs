using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using System;
using System.Linq;

namespace RazorPagesMovie1.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RazorPagesMovie1Context(
                serviceProvider.GetRequiredService<DbContextOptions<RazorPagesMovie1Context>>()))
            {
                // Seed Directors
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

                // Seed Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(
                        new Actor { FirstName = "Leonardo", LastName = "DiCaprio", BirthDate = new DateTime(1974, 11, 11), Nationality = "American" },
                        new Actor { FirstName = "Samuel", LastName = "Jackson", BirthDate = new DateTime(1948, 12, 21), Nationality = "American" },
                        new Actor { FirstName = "Meg", LastName = "Ryan", BirthDate = new DateTime(1961, 11, 19), Nationality = "American" },
                        new Actor { FirstName = "Matthew", LastName = "McConaughey", BirthDate = new DateTime(1969, 11, 4), Nationality = "American" },
                        new Actor { FirstName = "John", LastName = "Travolta", BirthDate = new DateTime(1954, 2, 18), Nationality = "American" },
                        new Actor { FirstName = "Jamie", LastName = "Foxx", BirthDate = new DateTime(1967, 12, 13), Nationality = "American" }
                    );
                    context.SaveChanges();
                }

                // Seed Movies
                if (!context.Movie.Any())
                {
                    var directors = context.Director.ToList();
                    var actors = context.Actors.ToList();

                    context.Movie.AddRange(
                        new Movie
                        {
                            Title = "When Harry Met Sally",
                            ReleaseDate = new DateTime(1989, 2, 12),
                            Genre = "Romantic Comedy",
                            Price = 7.99M,
                            Rating = "PG",
                            DirectorId = directors.First(d => d.Name == "Rob Reiner").Id,
                            ActorId = actors.First(a => a.FirstName == "Meg" && a.LastName == "Ryan").Id,
                            ImageUrl = "whenharrymetsally.jpg"
                        },
                        new Movie
                        {
                            Title = "Inception",
                            ReleaseDate = new DateTime(2010, 7, 16),
                            Genre = "Sci-Fi",
                            Price = 9.99M,
                            Rating = "PG",
                            DirectorId = directors.First(d => d.Name == "Moriswi Simon").Id,
                            ActorId = actors.First(a => a.FirstName == "Leonardo" && a.LastName == "DiCaprio").Id,
                            ImageUrl = "inception.jpg"
                        },
                        new Movie
                        {
                            Title = "E.T. the Extra-Terrestrial",
                            ReleaseDate = new DateTime(1982, 6, 11),
                            Genre = "Science Fiction",
                            Price = 7.99M,
                            Rating = "PG",
                            DirectorId = directors.First(d => d.Name == "Lerato Lee").Id,
                            ActorId = actors.First(a => a.FirstName == "Meg" && a.LastName == "Ryan").Id,
                            ImageUrl = "et.jpg"
                        },
                        new Movie
                        {
                            Title = "Interstellar",
                            ReleaseDate = new DateTime(2014, 11, 7),
                            Genre = "Science Fiction",
                            Price = 12.99M,
                            Rating = "PG",
                            DirectorId = directors.First(d => d.Name == "Lucy Mmasa").Id,
                            ActorId = actors.First(a => a.FirstName == "Matthew" && a.LastName == "McConaughey").Id,
                            ImageUrl = "interstellar.jpg"
                        },
                        new Movie
                        {
                            Title = "Pulp Fiction",
                            ReleaseDate = new DateTime(1994, 10, 14),
                            Genre = "Crime",
                            Price = 8.99M,
                            Rating = "PG",
                            DirectorId = directors.First(d => d.Name == "Mpho Nkuna").Id,
                            ActorId = actors.First(a => a.FirstName == "Samuel" && a.LastName == "Jackson").Id,
                            ImageUrl = "pulpfiction.jpg"
                        },
                        new Movie
                        {
                            Title = "Django Unchained",
                            ReleaseDate = new DateTime(2012, 12, 25),
                            Genre = "Western",
                            Price = 11.99M,
                            Rating = "PG",
                            DirectorId = directors.First(d => d.Name == "Mothiba Fortunate").Id,
                            ActorId = actors.First(a => a.FirstName == "Jamie" && a.LastName == "Foxx").Id,
                            ImageUrl = "django.jpg"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
