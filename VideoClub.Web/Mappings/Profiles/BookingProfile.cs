using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;
using VideoClub.Web.Areas.Bookings.Models;

namespace VideoClub.Web.Mappings.Profiles
{
    public class BookingProfile : AutoMapper.Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingBindingModel, Booking>().ReverseMap();
        }
    }
}