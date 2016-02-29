using Horse.HubInterface;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using Horse.TransportModel;
using System.Collections.ObjectModel;
using Horse.Agent.Models;

namespace Horse.Agent
{
   
    public class AgentHub:Hub
    {



        public void RunTask(AgentTask t)
        {
            Clients.All.sayHello();
        }

        public List<AppInfo> GetSoftwares()
        {
            return Utils.GetSoftwares();
        }

        public void SendMessage(ChatMessage msg)
        {
            msg.LoginDt = DateTime.Now.ToLongTimeString();
            Clients.All.addMessage(msg);
        }

        public override Task OnConnected()
        {
            var userInfo = Context.QueryString["name"];
            var url = Context.QueryString["url"];
            Clients.All.addUser(userInfo, url);

            Application.Current.Dispatcher.Invoke(() => {      
                ((MainWindow)Application.Current.MainWindow).AddClient(new ClientDetailInfo(Context.ConnectionId,Context.User.Identity.Name,DateTime.Now));
            });
            
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            Application.Current.Dispatcher.Invoke(() => {
                ((MainWindow)Application.Current.MainWindow).RemoveClient(Context.ConnectionId);
            });
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userInfo = Context.QueryString["name"];
            Clients.All.removeUser(userInfo);
            Application.Current.Dispatcher.Invoke(() => {
                ((MainWindow)Application.Current.MainWindow).RemoveClient(Context.ConnectionId);
            });
            return base.OnDisconnected(stopCalled);
        }
    }
}
