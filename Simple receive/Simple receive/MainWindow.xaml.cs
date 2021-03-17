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
        
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int Port = 8000;
        TcpListener host = null;
        
        public MainWindow()
        {
            InitializeComponent();
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

                Byte[] bytes = new Byte[256];


                while (true)
                {
                    Dispatcher.Invoke(() => { textBlock.Text += "\nVerbinde..."; });
                    TcpClient client = host.AcceptTcpClient();
                    Dispatcher.Invoke(() => { textBlock.Text += "\nVerbunden..."; });

                    NetworkStream stream = client.GetStream();
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        byte[] boolteil = new byte[1];
                        boolteil[0] = bytes[0];

                        bool empfbool = BitConverter.ToBoolean(boolteil, 0);

                        Int16 intteil = BitConverter.ToInt16(bytes, 1);

                        if (empfbool)
                        {
                            Dispatcher.Invoke(() => { textBlock.Text += "\n" + intteil; });
                        }



                    }
                }

            }catch(Exception e)
            {
                Dispatcher.Invoke(() => { textBlock.Text += "\n Exception: " + e; });

            }
            finally
            {
                host.Stop();
            }
        }
    }
}
