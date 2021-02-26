using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Core.Enums;

namespace VideoClub.Core.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MovieGenre Genre { get; set; }
        public List<DVD> DVDs { get; set; }

        public Movie()
        {
            DVDs = new List<DVD>();
        }

        public Movie(string title, string description, MovieGenre genre)
        {
            Title = title;
            Description = description;
            Genre = genre;
            DVDs = new List<DVD>();
        }
    }
}
