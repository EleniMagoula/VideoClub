using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoClub.Common.Services;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;
using VideoClub.Infrastructure.Services;
using VideoClub.Web.Mappings;
using VideoClub.Web.Mappings.Profiles;

namespace VideoClub.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Services
            builder.RegisterType<MovieService>()
                .As<IMovieService>()
                .InstancePerRequest();

            builder.RegisterType<BookingService>()
                .As<IBookingService>()
                .InstancePerRequest();

            builder.RegisterType<CustomerService>()
                .As<ICustomerService>()
                .InstancePerRequest();

            builder.RegisterType<DVDService>()
                .As<IDVDService>()
                .InstancePerRequest();

            builder.RegisterType<MoviePagingService>()
                .As<IMoviePagingService>()
                .InstancePerRequest();

            // Mapper
            builder.Register(c => MapperInit.Init())
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MovieProfile());
                cfg.AddProfile(new BookingProfile());
            });

            builder.RegisterInstance(mapperConfig.CreateMapper())
                .As<IMapper>()
                .SingleInstance();

            // Logging Service
            builder.RegisterType<LoggingService>()
                .As<ILoggingService>()
                .InstancePerRequest();

            // Db Context
            builder.RegisterType<ApplicationDbContext>()
                .As<ApplicationDbContext>()
                .InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}