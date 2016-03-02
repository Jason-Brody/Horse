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

        public static void RegisterAddress()
        {
            HttpClientHelper.BaseURL = "http://localhost:36149/";

            var a = HttpClientHelper.GetInfo<List<string>>("api/testdata").Result;
        }
    }
}
