using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineMovie.Startup))]
namespace OnlineMovie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
