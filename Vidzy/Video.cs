using System;
using System.Collections.Generic;
using System.Linq;

namespace Vidzy
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual Genre Genre { get; set; }
        public byte Genre_Id { get; set; }
        public Classification Classification { get; set; }
        public virtual ICollection<Tag> Tags { get; private set; }

        public Video()
        {
            Tags = new HashSet<Tag>();
        }
        public void AddTag(Tag tag)
        {
            Tags.Add(tag);
        }
        public void RemoveTag(string name)
        {
            var oneTag = Tags.SingleOrDefault(n => n.Name == name);

            Tags.Remove(oneTag);
        }
    }
}