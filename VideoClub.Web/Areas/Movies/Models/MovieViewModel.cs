using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoClub.Core.Entities;
using VideoClub.Core.Enums;

namespace VideoClub.Web.Areas.Movies.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MovieGenre Genre { get; set; }
        public IEnumerable<DVD> DVDs { get; set; }
        public List<SelectListItem> TestItems { get; set; }

        public MovieViewModel(int id, string title, string description, MovieGenre genre, List<DVD> dvds)
        {
            Id = id;
            Title = title;
            Description = description;
            Genre = genre;
            DVDs = dvds;
        }

        public MovieViewModel()
        {

        }
    }

}