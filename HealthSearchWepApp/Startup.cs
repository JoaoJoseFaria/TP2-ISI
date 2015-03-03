using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthSearchWepApp.Startup))]
namespace HealthSearchWepApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
