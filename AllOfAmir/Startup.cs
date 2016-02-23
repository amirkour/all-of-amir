using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AllOfAmir.Startup))]
namespace AllOfAmir
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
