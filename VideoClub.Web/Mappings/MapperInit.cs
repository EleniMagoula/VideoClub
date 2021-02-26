using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClub.Web.Mappings.Profiles;

namespace VideoClub.Web.Mappings
{
    public class MapperInit
    {
        public static IMapper Init()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MovieProfile());
                cfg.AddProfile(new BookingProfile());
            });

            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}