using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebMidterm.Startup))]
namespace WebMidterm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
