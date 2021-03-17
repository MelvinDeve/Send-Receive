using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ArschlochClient.Classes
{
    class SimpleSend
    {
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

    }
}
