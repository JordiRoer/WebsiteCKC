using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteCKC.Startup))]
namespace WebsiteCKC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
