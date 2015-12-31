using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Web.Common.SignalR.Startup))]

namespace Web.Common.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}