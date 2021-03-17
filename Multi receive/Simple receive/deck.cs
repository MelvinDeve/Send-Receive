using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_receive
{
    class deck
    {
        private List<Card> Deck = new List<Card>();
        private int rounds = 0;

        public deck()
        {
            createDeck();
        }

        private void createDeck()
        {
            string cardcolour = "herz";
            
            while (rounds != 4)
            {
                if (rounds == 1)
                {
                    cardcolour = "blatt";
                }
                else if (rounds == 2)
                {
                    cardcolour = "schelle";
                }
                else
                {
                    cardcolour = "eichel";
                }

                for (int i = 6; i < 15; i++)
                {
                    Deck.Add(new Card(i, cardcolour));

                }
                rounds++;
            }
        }

        private void resetDeck()
        {
            for (int i =0; i<Deck.Count;i++)
            {
                Deck[i].handedOut = false;
            }
        }

        private int[] handoutCard (int numOfPlayers)
        {
            Random rand = new Random();
            int toBehanded = 36 / numOfPlayers;
            int[] handcards = new int[toBehanded];
            for (int i=0; i<8; i++)
            {
                
                int currentcard = rand.Next(36);
                while (Deck[currentcard].handedOut == true)
                {
                    currentcard = rand.Next(36);
                }
              
                    handcards[i] = Deck[currentcard].id;
                    Deck[currentcard].handedOut = true;
                
            }
            return handcards;
            
        }
    }
}
