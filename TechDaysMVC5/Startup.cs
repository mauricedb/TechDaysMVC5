using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechDaysMVC5.Startup))]
namespace TechDaysMVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
