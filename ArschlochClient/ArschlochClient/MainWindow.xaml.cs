﻿using System;
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

namespace ArschlochClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IPAddress ipAddress = IPAddress.Parse("192.168.178.28");
        int Port = 8000;
        TcpListener host = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            if (host == null)
            {
                Thread worker = new Thread(getCards);
                worker.Start();
            }
        }

        public void getCards()
        {
            try
            {
                host = new TcpListener(ipAddress, Port);
                host.Start();

                while (true)
                {
                    TcpClient client = host.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    int i;

                    Byte[] bytes = new Byte[256];
                    List<int> cards = new List<int>();
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        int j = 0;
                        while (bytes[j] != 0)
                        {
                            cards.Add(BitConverter.ToInt32(bytes, j));
                            j += 4;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Dispatcher.Invoke(() => { textBlock.Text += "\n Exception: " + e; });

            }
        }
        /*
        public void getResponse()
        {
            try
            {
                host = new TcpListener(ipAddress, Port);
                host.Start();

                Byte[] bytes = new Byte[256];
                byte[] boolteil = new byte[1];
                bool empfbool = false;
                int cardId = 0;
                Int16 cardAmount = 0;

                while (!empfbool)
                {
                    TcpClient client = host.AcceptTcpClient();
           
                    NetworkStream stream = client.GetStream();
                    int i;
                    
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        boolteil[0] = bytes[0];
                        empfbool = BitConverter.ToBoolean(boolteil, 0);
                        cardId = BitConverter.ToInt32(bytes, 1);
                        cardAmount = BitConverter.ToInt16(bytes, 5);
                    }
                }
            }
            catch (Exception e)
            {
                Dispatcher.Invoke(() => { textBlock.Text += "\n Exception: " + e; });

            }
        }
        */
        /*
        private void send_btn_Click(object sender, RoutedEventArgs e)
        {
            Thread worker = new Thread(sendPerThread);
            worker.Start();
        }
        */
        /*
        private void sendPerThread()
        {
            byte[] buffer = ASCIIEncoding.UTF8.GetBytes(toSend);

            NetworkStream nwStream = host.GetStream();
            nwStream.Write(buffer, 0, buffer.Length);

        }
        */

        private void textBlock_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
    }
}
