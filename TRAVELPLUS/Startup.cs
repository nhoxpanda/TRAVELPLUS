using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TRAVELPLUS.Startup))]
namespace TRAVELPLUS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
