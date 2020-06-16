using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(T1809E_PROJECT_SEM3.Startup))]
namespace T1809E_PROJECT_SEM3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
