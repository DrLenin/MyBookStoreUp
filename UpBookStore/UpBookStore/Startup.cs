using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UpBookStore.Startup))]
namespace UpBookStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
