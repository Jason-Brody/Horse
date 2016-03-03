using Horse.TransportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse.Agent.Utils
{
    static class ClientRegister
    {
        
        const string _url = "";

        public static async void RegisterAddress()
        {
            HttpClientHelper.BaseURL = "http://192.168.2.3:48980/";

            RegisterInfo info = new RegisterInfo();
            info.IPAddress = Tools.GetLocalIPAddress();
            info.HostName = System.Net.Dns.GetHostEntry("").HostName;
            var result = await HttpClientHelper.PostInfo<RegisterInfo>("api/clientregister/register", info);

            
        }
    }
}
