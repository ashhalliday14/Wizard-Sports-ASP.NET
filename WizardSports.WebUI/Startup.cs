using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WizardSports.WebUI.Startup))]
namespace WizardSports.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
