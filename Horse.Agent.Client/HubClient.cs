using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Horse.Agent.Client
{
    class HubClient
    {
        private string _server;
        public HubClient(string Server)
        {
            this._server = Server;
        }

        private IHubProxy _proxy;

        private HubConnection _connection;

        public IHubProxy Proxy { get { return _proxy; } }

        public void Connect()
        {
            connectAsync();
        }

        private async void connectAsync()
        {
            _connection = new HubConnection(_server, useDefaultUrl: false);
            _connection.Credentials = CredentialCache.DefaultCredentials;
           
            _proxy = _connection.CreateHubProxy("agentHub");

            //_proxy.On<TaskProcess>("ReturnTaskInfo", p=> {
            //    copyTask(_tps, p);
            //});

            await _connection.Start();
        }

        public void DisConnect()
        {
            if (_connection != null)
            {
                _connection.Stop();
                _connection.Dispose();
            }
        }
    }
}
