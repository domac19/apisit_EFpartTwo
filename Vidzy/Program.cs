using System;
using System.Linq;
using System.Data.Entity;

namespace Vidzy
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new VidzyContext();

            var showMovie = dbContext.Videos.Where(m => m.Genre.Name == "Action").OrderBy(m => m.Name);

            foreach (var movie in showMovie)
            {
                Console.WriteLine(movie.Name);
            }

            var dramaMovies = dbContext.Videos.Where(m => m.Genre.Name == "Drama" && m.Classification == Classification.Gold).OrderByDescending(m => m.ReleaseDate);

            foreach (var drama in dramaMovies)
            {
                Console.WriteLine(drama.Name);
            }

            var allMovies = dbContext.Videos.Select(m => new { MovieName = m.Name, Genre = m.Genre.Name });

            foreach (var all in allMovies)
            {
                Console.WriteLine(all.MovieName);
            }

            var groups = dbContext.Videos
                            .GroupBy(v => v.Classification)
                            .Select(g => new
                            {
                                Classification = g.Key.ToString(),
                                Videos = g.OrderBy(v => v.Name)
                            });

            foreach (var g in groups)
            {
                Console.WriteLine("Classification: " + g.Classification);

                foreach (var v in g.Videos)
                    Console.WriteLine("\t" + v.Name);
            }

            var classifications = dbContext.Videos
                                    .GroupBy(v => v.Classification)
                                    .Select(g => new
                                    {
                                        Name = g.Key.ToString(),
                                        VideosCount = g.Count()
                                    })
                                    .OrderBy(c => c.Name);

            foreach (var c in classifications)
                Console.WriteLine("{0} ({1})", c.Name, c.VideosCount);

            var genres = dbContext.Genres
                            .GroupJoin(dbContext.Videos, g => g.Id, v => v.Genre_Id, (genre, videos) => new
                            {
                                Name = genre.Name,
                                VideosCount = videos.Count()
                            })
                            .OrderByDescending(g => g.VideosCount);

            foreach (var g in genres)
                Console.WriteLine("{0} ({1})", g.Name, g.VideosCount);

            /* Lazy loading */
            var lazyLoading = dbContext.Videos.ToList();

            foreach (var lazy in lazyLoading)
            {
                Console.WriteLine(lazy.Name, lazy.Genre.Name);
            }

            /* Eager loading */
            var eagerLoading = dbContext.Videos.Include(g => g.Genre).ToList();

            foreach (var eager in eagerLoading)
            {
                Console.WriteLine(eager.Name, eager.Genre.Name);
            }

            /* Explicit loading */

            dbContext.Genres.Load();

            foreach (var item in lazyLoading)
            {
                Console.WriteLine(item.Name, item.Genre.Name);
            }
        }
    }
}
