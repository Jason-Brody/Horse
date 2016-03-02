using Horse.TransportModel;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace Horse.Agent.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IHubProxy _proxy;

        //private string _server = "http://localhost:9000/signalr";
        private string _server = "http://15.107.23.67:9000/signalr";
        private HubConnection _connection;

        //private TaskProcess _tps;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void connectAsync()
        {
            _connection = new HubConnection(_server,useDefaultUrl:false);
            _connection.Credentials = CredentialCache.DefaultCredentials;
            _connection.Closed += _connection_Closed;
            _proxy = _connection.CreateHubProxy("chatHub");
            
          
            //_proxy.On<TaskProcess>("ReturnTaskInfo", p=> {
            //    copyTask(_tps, p);
            //});

            await _connection.Start();
        }

        private void _connection_Closed()
        {
            //throw new NotImplementedException();
        }

        private void btn_Connect_Click(object sender, RoutedEventArgs e)
        {
            connectAsync();
            btn_Connect.IsEnabled = false;
        }

        private async void btn_RunTask_Click(object sender, RoutedEventArgs e)
        {
            //var result =await _proxy.Invoke<List<AppInfo>>("GetSoftwares");
            //var a = result;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(_connection!=null)
            {
                _connection.Stop();
                _connection.Dispose();
            }
        }



        //private void copyTask(TaskProcess t1,TaskProcess t2)
        //{
        //    t1.CurrentProcess = t2.CurrentProcess;
        //    t1.TotalProcess = t2.TotalProcess;
        //    t1.IsIndeterminate = t2.IsIndeterminate;
        //}
    }
}
