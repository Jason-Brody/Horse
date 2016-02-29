using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Horse.Agent
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpListener listener = (HttpListener)app.Properties["System.Net.HttpListener"];
            listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;

            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
