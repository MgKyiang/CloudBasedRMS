using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudBasedRMS.View.Startup))]
namespace CloudBasedRMS.View
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
