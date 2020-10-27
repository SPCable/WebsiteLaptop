using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebVL.Startup))]
namespace WebVL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
