using Horse.Agent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace Horse.Software
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Get_Click(object sender, RoutedEventArgs e)
        {
            InstalledPrograms.GetInstalledPrograms();
            MessageBox.Show("Finished");
        }

        private void btn_Show_Click(object sender, RoutedEventArgs e)
        {
            var folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SoftwareInfo");
            Process.Start(folder);
        }
    }
}
