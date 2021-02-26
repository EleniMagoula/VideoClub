using Microsoft.Owin;
using Owin;
using Serilog;
using VideoClub.Infrastructure.Services;

[assembly: OwinStartupAttribute(typeof(VideoClub.Web.Startup))]
namespace VideoClub.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
