using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArschlochHost
{
    class player
    {
        private string name;
        private int[] handcards;

        player(string name, int numofPlayers)
        {
            this.name = name;
            handcards = deck.handoutCards(numofPlayers);

        }

        public int[] getHandCards()
        {
            return handcards;
        }
    }
}
