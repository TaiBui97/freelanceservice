using Owin;
using Microsoft.Owin;
using DACN.Common;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(SignalRChat.Startup))]
namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(typeof(DACN.Common.IUserIdProvider), () => new CustomUserIdProvider());

            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}