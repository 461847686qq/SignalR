using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using MVCSignalR.Hubs;

[assembly: OwinStartup(typeof(MVCSignalR.Startup))]

namespace MVCSignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapConnection<MyConnection>("/myConnection");
            app.MapHubs();

        }
    }
}
