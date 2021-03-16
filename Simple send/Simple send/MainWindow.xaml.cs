using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace Simple_send
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String ServerIp = "127.0.0.1";
        int Port = 8000;
        TcpClient client = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void connect_btn_Click(object sender, RoutedEventArgs e)
        {
            if(client == null)
            {
                client = new TcpClient();
                client.Connect(ServerIp, Port);
            }
        }

        private void send_btn_Click(object sender, RoutedEventArgs e)
        {
            Thread worker = new Thread(sendPerThread);
            worker.Start();
        }

        private void sendPerThread()
        {
            
            string toSend = "";
            Dispatcher.Invoke(() => { toSend = textBox.Text; });
            byte[] buffer = ASCIIEncoding.UTF8.GetBytes(toSend);

            NetworkStream nwStream = client.GetStream();
            nwStream.Write(buffer, 0, buffer.Length);
        }

        private void disconnect_btn_Click(object sender, RoutedEventArgs e)
        {
            if (client != null)
            {
                client.Close();
                client = null;
            }
        }
    }
}
