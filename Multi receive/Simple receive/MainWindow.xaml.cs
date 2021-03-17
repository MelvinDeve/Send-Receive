using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace Simple_receive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Socket> _clients = new List<Socket>();
        bool _isConnected;
        bool drRunning = false;
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int Port = 8000;
        TcpListener host = null;
        
        public MainWindow()
        {
            InitializeComponent();
            deck testDeck = new deck();
            int[] hand1 = testDeck.handoutCard(4);
            int[] hand2 = testDeck.handoutCard(4);
            int[] hand3 = testDeck.handoutCard(4);
            int[] hand4 = testDeck.handoutCard(4);
            testDeck.resetDeck();

        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            Dispatcher.Invoke(() => { textBlock.Text = ""; });
            Thread worker = new Thread(getResponse);
            worker.Start();
        }

        private void getResponse()
        {
            try
            {
                host = new TcpListener(ipAddress, Port);
                host.Start();

                while (true)
                {
                    Dispatcher.Invoke(() => { textBlock.Text += "\nVerbinde..."; });
                    Socket client = host.AcceptSocket();
                    _clients.Add(client);
                    _isConnected = true;
                    Dispatcher.Invoke(() => { textBlock.Text += "\nVerbunden..." + _clients.Count; });
                    if (!drRunning)
                    {
                        Thread worker = new Thread(DataReceive);
                        worker.Start();
                    }
                }

            }catch(Exception e)
            {
                Dispatcher.Invoke(() => { textBlock.Text += "\n Exception: " + e; });

            }
            finally
            {
                host.Stop();
                _isConnected = false;
            }
        }

        private void DataReceive()
        {
            drRunning = true;
            while (_isConnected)
            {
                List<Socket> clients = new List<Socket>(_clients);
                foreach (Socket client in clients)
                {
                    try
                    {
                        if (!client.Connected)
                        {
                            _clients.Remove(client);
                            continue;
                        }
                        string txt = "";
                        while (client.Available > 0)
                        {
                            byte[] bytes = new byte[client.ReceiveBufferSize];
                            int byteRec = client.Receive(bytes);
                            if (byteRec > 0)
                                txt += Encoding.UTF8.GetString(bytes, 0, byteRec);
                        }
                        if (!string.IsNullOrEmpty(txt))
                        {
                            Dispatcher.Invoke(() => { textBlock.Text += "\n" + txt; });
                        }
                        
                }
                    catch (Exception e)
                    {
                        Dispatcher.Invoke(() => { textBlock.Text += "\n Exception: " + e; });
                    }
                }
            }
            drRunning = false;
        }


        }
}
