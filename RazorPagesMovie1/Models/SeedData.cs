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

                // Seed Movies (assign single actor)
                if (!context.Movie.Any())
                {
                    var robReiner = context.Director.First(d => d.Name == "Rob Reiner");
                    var moriswiSimon = context.Director.First(d => d.Name == "Moriswi Simon");
                    var leratoLee = context.Director.First(d => d.Name == "Lerato Lee");
                    var lucyMmasa = context.Director.First(d => d.Name == "Lucy Mmasa");
                    var mphoNkuna = context.Director.First(d => d.Name == "Mpho Nkuna");
                    var mothibaFortunate = context.Director.First(d => d.Name == "Mothiba Fortunate");

                    var leo = context.Actors.First(a => a.FirstName == "Leonardo" && a.LastName == "DiCaprio");
                    var samuel = context.Actors.First(a => a.FirstName == "Samuel" && a.LastName == "Jackson");
                    var meg = context.Actors.First(a => a.FirstName == "Meg" && a.LastName == "Ryan");
                    var matthew = context.Actors.First(a => a.FirstName == "Matthew" && a.LastName == "McConaughey");
                    var john = context.Actors.First(a => a.FirstName == "John" && a.LastName == "Travolta");
                    var jamie = context.Actors.First(a => a.FirstName == "Jamie" && a.LastName == "Foxx");

                    context.Movie.AddRange(
                        new Movie { Title = "When Harry Met Sally", ReleaseDate = new DateTime(1989, 2, 12), Genre = "Romantic Comedy", Price = 7.99M, DirectorId = robReiner.Id, ActorId = meg.Id },
                        new Movie { Title = "Inception", ReleaseDate = new DateTime(2010, 7, 16), Genre = "Sci-Fi", Price = 9.99M, DirectorId = moriswiSimon.Id, ActorId = leo.Id },
                        new Movie { Title = "E.T. the Extra-Terrestrial", ReleaseDate = new DateTime(1982, 6, 11), Genre = "Science Fiction", Price = 7.99M, DirectorId = leratoLee.Id, ActorId = meg.Id },
                        new Movie { Title = "Interstellar", ReleaseDate = new DateTime(2014, 11, 7), Genre = "Science Fiction", Price = 12.99M, DirectorId = lucyMmasa.Id, ActorId = matthew.Id },
                        new Movie { Title = "Pulp Fiction", ReleaseDate = new DateTime(1994, 10, 14), Genre = "Crime", Price = 8.99M, DirectorId = mphoNkuna.Id, ActorId = samuel.Id },
                        new Movie { Title = "Django Unchained", ReleaseDate = new DateTime(2012, 12, 25), Genre = "Western", Price = 11.99M, DirectorId = mothibaFortunate.Id, ActorId = jamie.Id }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
