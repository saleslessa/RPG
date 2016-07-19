using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DaemonCharacter.UI.MVC.Startup))]
namespace DaemonCharacter.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
