using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PresentationMVC.Startup))]
namespace PresentationMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
