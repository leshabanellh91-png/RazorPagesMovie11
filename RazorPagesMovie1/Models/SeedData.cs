using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Models;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;
using System.IO;

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
                    var MoriswiSimon = context.Director.First(d => d.Name == "Moriswi Simon");
                    var LeratoLee = context.Director.First(d => d.Name == "Lerato Lee");
                    var LucyMmasa = context.Director.First(d => d.Name == "Lucy Mmasa");
                    var MphoNkuna = context.Director.First(d => d.Name == "Mpho Nkuna");
                    var MothibaFortunate = context.Director.First(d => d.Name == "Mothiba Fortunate");

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
                            DirectorId = MoriswiSimon.Id
                        },
                        new Movie
                        {
                            Title = "E.T. the Extra-Terrestrial",
                            Genre = "Science Fiction",
                            ReleaseDate = new DateTime(1982, 6, 11),
                            Price = 7.99M,
                            DirectorId = LeratoLee.Id
                        },
                         new Movie
                         {
                             Title = "Interstellar",
                             Genre = "Science Fiction",
                             ReleaseDate = new DateTime(2014, 11, 7),
                             Price = 12.99M,
                             DirectorId = LucyMmasa.Id
                         },
                    new Movie
                    {
                        Title = "Pulp Fiction",
                        Genre = "Crime",
                        ReleaseDate = new DateTime(1994, 10, 14),
                        Price = 8.99M,
                        DirectorId = MphoNkuna.Id
                    },
                    new Movie
                    {
                        Title = "Django Unchained",
                        Genre = "Western",
                        ReleaseDate = new DateTime(2012, 12, 25),
                        Price = 11.99M,
                        DirectorId = MothibaFortunate.Id
                    }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
