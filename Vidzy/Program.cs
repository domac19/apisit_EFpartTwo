using System;
using System.Linq;
using System.Data.Entity;

namespace Vidzy
{
    class Program
    {
        static void Main(string[] args)
        {

            AddVideo(new Video
            {
                Name = "Terminator 1",
                ReleaseDate = new DateTime(1984, 10, 26),
                Genre_Id = 2,
                Classification = Classification.Silver
            });

            AddTags("classics","drama");

            AddVideoTags("classics", "drama", "comedy");

            Remove();

            RemoveVideo(1);

            RemoveGenre(2, true);
        }

        public static void AddVideo(Video video)
        {
            using (var dbContext = new VidzyContext())
            {
                dbContext.Videos.Add(video);
                dbContext.SaveChanges();
            }
        }

        public static void AddTags(params string[] twoTags)
        {
            using(var dbContext = new VidzyContext())
            {
                
            }
        }
        public static void AddVideoTags(params string[] threeTags)
        {
            using(var dbContext = new VidzyContext())
            {

            }
        }
        public static void Remove()
        {
            using(var dbContext = new VidzyContext())
            {

            }
        }
        public static void RemoveVideo(int id)
        {
            using (var dbContext = new VidzyContext())
            {
                var videos = dbContext.Videos.SingleOrDefault(r => r.Id == id);

                dbContext.Videos.Remove(videos);
                dbContext.SaveChanges();
            }
        }
        public static void RemoveGenre(int id, bool enforceVideos)
        {
            using (var dbContext = new VidzyContext())
            {
                var genres = dbContext.Genres.Include(g => g.Videos).SingleOrDefault(g => g.Id == id);

                if (enforceVideos) dbContext.Videos.RemoveRange(genres.Videos);

                dbContext.Genres.Remove(genres);
                dbContext.SaveChanges();
            }
        }
    }
}
