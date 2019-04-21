using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aviato.Startup))]
namespace Aviato
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
