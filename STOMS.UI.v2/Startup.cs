using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(STOMS.UI.v2.Startup))]
namespace STOMS.UI.v2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
