using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Facebook.WebUI.Startup))]

namespace Facebook.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(); //Owin sadece Signalr ile Çalışan bir yapı değil burada signalr ile çaşışacağımızı belirttik.
        }
    }
}
