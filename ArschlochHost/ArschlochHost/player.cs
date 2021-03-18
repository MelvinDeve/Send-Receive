using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ArschlochHost
{
    class player
    {
        private string name;
        private int[] handcards;

        public player(string name, int numofPlayers)
        {
            this.name = name;
            Application.Current.Dispatcher.Invoke(() => { this.handcards = deck.handoutCards(numofPlayers); });
            //handcards = deck.handoutCards(numofPlayers);
        }

        public int[] getHandCards()
        {
            return handcards;
        }
    }
}
