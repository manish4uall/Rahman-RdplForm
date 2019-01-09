using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RdplForm.Startup))]
namespace RdplForm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
