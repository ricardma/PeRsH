using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrabFinal___PeRsH.Startup))]
namespace TrabFinal___PeRsH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
