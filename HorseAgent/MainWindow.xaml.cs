using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Horse.TransportModel;
using System.Collections.ObjectModel;
using Microsoft.Owin.Hosting;
using Horse.Agent.Models;
using System.Net;
using System.Net.Sockets;
using Horse.Agent.Utils;

namespace Horse.Agent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ClientDetailInfo> _clientList = new ObservableCollection<ClientDetailInfo>();
        private IDisposable _singalR;
        //private string _serverURL = "http://localhost:9000";
       // private string _serverURL = "http://192.168.2.3:9000";
        public MainWindow()
        {
            InitializeComponent();
            lv_Clients.ItemsSource = _clientList;
            //ClientRegister.RegisterAddress();
         
        }

        private void TaskList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            startServer();
        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            _singalR.Dispose();
            btn_Start.IsEnabled = true;
        }

        private void startServer()
        {
            StartOptions options = new StartOptions();

            options.Urls.Add("http://localhost:9000");
            options.Urls.Add("http://127.0.0.1:9000");
            //options.Urls.Add("http://15.107.23.67:9000");
            options.Urls.Add(string.Format("http://{0}:9000", System.Net.Dns.GetHostEntry("").HostName));
            _singalR = WebApp.Start<Startup>(options);
            btn_Start.IsEnabled = false;

        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
        public void AddClient(ClientDetailInfo client)
        {
            _clientList.Add(client);
        }

        public void RemoveClient(string ConnectionId)
        {
            var client = _clientList.Where(s => s.ConnectionId == ConnectionId).FirstOrDefault();
            if (client != null)
                _clientList.Remove(client);
        }


    }
}
