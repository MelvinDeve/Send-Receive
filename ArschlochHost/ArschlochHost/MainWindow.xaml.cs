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


namespace ArschlochHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Socket> _clients = new List<Socket>();
        private List<player> players = new List<player>();
        bool _isConnected;
        bool drRunning = false;
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int Port = 8000;
        TcpListener host = null;
        private int currentCard = 0;
        private Int16 ammount = 0;
        private bool listening = false;
        private bool playersConnecting = true;
        int currentPlayer = -1;



        public MainWindow()
        {
            InitializeComponent();
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            Thread worker = new Thread(getResponse);
            worker.Start();
        }

        private void getResponse()
        {
            try
            {
                host = new TcpListener(ipAddress, Port);
                host.Start();

                while (playersConnecting)
                {
                    Socket client = host.AcceptSocket();
                    _clients.Add(client);
                    _isConnected = true;
                    Dispatcher.Invoke(() => { textBlock.Text = "Connected Players: " + _clients.Count; });
                }

            }
            catch (Exception e)
            {
                Dispatcher.Invoke(() => { textBlock.Text += "\n Exception: " + e; });

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

        private void createPlayerList()
        {
            for(int i=0; i<_clients.Count; i++)
            {
                players.Add(new player("player" + (i + 1), _clients.Count));
            }
        }

        private void publishPlayerHands()
        {
            for(int i = 0; i < _clients.Count; i++)
            {
                byte[] byteCards = new byte[players[i].getHandCards().Length*4];
                byte[] tempByteCards = new byte[4];
                int byteCardsPos = 0;
                foreach(int card in players[i].getHandCards())
                {
                    if(card == 1)
                    {
                        currentPlayer = i;
                    }
                    tempByteCards = BitConverter.GetBytes(card);
                    int m = byteCardsPos + 4;
                    int tempByteCardsPos = 0;
                    while (byteCardsPos < m)
                    {
                        byteCards[byteCardsPos] = tempByteCards[tempByteCardsPos];
                        byteCardsPos++;
                        tempByteCardsPos++;
                    }
                }
                _clients[i].Send(byteCards);
            }
        }

        private void gameManager()
        {
            if (_clients.Count > 1)
            {
                playersConnecting = false;
                createPlayerList();
                publishPlayerHands();
            }
            else
            {
                return;
            }
            if(currentCard == 0)
            {

            }
            else
            {

            }
        }

        private void getStartingPlayer()
        {

        }

        private void publishTurn(int player)
        {
            int playercount = 0;
            List<Socket> clients = new List<Socket>(_clients);
            foreach (Socket client in clients)
            {
                bool playerTurn = false;
                if (playercount == player)
                {
                    playerTurn = true;
                }
                byte[] message = new byte[7];
                message[0] = BitConverter.GetBytes(playerTurn)[0];
                byte[] cardIdArr = new byte[4];
                cardIdArr = BitConverter.GetBytes(currentCard);
                message[1] = cardIdArr[0];
                message[2] = cardIdArr[1];
                message[3] = cardIdArr[2];
                message[4] = cardIdArr[3];
                byte[] ammountArr = new byte[2];
                ammountArr = BitConverter.GetBytes(ammount);
                message[5] = ammountArr[0];
                message[6] = ammountArr[1];

                client.Send(message);
            }
        }

        private void receiveTurn(int player)
        {
            List<Socket> clients = new List<Socket>(_clients);
            Socket client = clients[player];
            int cardId;
            int cardAmm;
            try
            {
                while (listening)
                {
                    byte[] bytes = new byte[client.ReceiveBufferSize];
                    int byteRec = client.Receive(bytes);

                    if (byteRec > 0)
                    {
                        byte[] cardIdArr = new byte[4];
                        for(int i = 0; i<4; i++)
                        {
                            cardIdArr[i] = bytes[i];
                        }
                        byte[] cardAmmArr = new byte[2];
                        for (int i = 0; i < 2; i++)
                        {
                            cardAmmArr[i] = bytes[i+4];
                        }
                        cardId = BitConverter.ToInt32(cardIdArr, 0);
                        cardAmm = BitConverter.ToInt16(cardAmmArr, 0);

                    }
                }

            }
            catch (Exception e)
            {
                Dispatcher.Invoke(() => { textBlock.Text += "\n Exception: " + e; });
            }
        }



    }

}
