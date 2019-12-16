using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FashionUControlPanel.Startup))]
namespace FashionUControlPanel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
