using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;
using VideoClub.Web.Areas.Movies.Models;

namespace VideoClub.Web.Mappings.Profiles
{
    public class MovieProfile : AutoMapper.Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieBindingModel>()
                .ForMember(dest => dest.MovieTitle,
                opt => opt.MapFrom(src => src.Title))
                .ReverseMap();
        }
    }
}