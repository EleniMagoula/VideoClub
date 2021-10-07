using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using VideoClub.Core.Enums;

namespace VideoClub.Web.Areas.Movies.Models
{
    public class MovieBindingModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string MovieTitle { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Διαλέξτε το είδος στο οποίο ανήκει.")]
        public MovieGenre Genre { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Η ταινία μπορεί να έχει από 1 έως 5 αντίτυπα.")]
        public int AvailableDVDs { get; set; }

        public SelectList Genres { get; set; }

        public string TestItem { get; set; }

        public List<SelectListItem> TestItems { get; set; }


        public MovieBindingModel()
        { }

        public MovieBindingModel(string title, string description, MovieGenre genre, int availableDVDs)
        {
            MovieTitle = title;
            Description = description;
            Genre = genre;
            AvailableDVDs = availableDVDs;
        }
    }

}