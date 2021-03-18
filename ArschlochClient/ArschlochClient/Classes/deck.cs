using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArschlochClient
{
   public static class deck
    {
        private static List<Card> Deck = createDeck();
        private static int rounds = 0;

        private static List<Card> createDeck()
        {
            List<Card> tmp = new List<Card>();
            string cardcolour = "herz";

            while (rounds != 4)
            {
                if (rounds == 0)
                {
                    cardcolour = "herz";
                }
                else if (rounds == 1)
                {
                    cardcolour = "karo";
                }
                else if (rounds == 2)
                {
                    cardcolour = "kreuz";
                }
                else
                {
                    cardcolour = "piek";
                }

                for (int i = 6; i < 15; i++)
                {
                    tmp.Add(new Card(i, cardcolour));

                }
                rounds++;
            }
            return tmp;
        }
        /// <summary>
        /// resets the deck
        /// </summary>
        public static void resetDeck()
        {
            for (int i = 0; i < Deck.Count; i++)
            {
                Deck[i].handedOut = false;
            }
        }

        /// <summary>
        /// returns an int array consisting of ids of the cards that should be held by the player
        /// one array for one player, random generated
        /// </summary>
        /// <param name="numOfPlayers"></param>
        /// <returns></returns>
        public static int[] handoutCards(int numOfPlayers)
        {
            Random rand = new Random();
            int toBehanded = 36 / numOfPlayers;
            int[] handcards = new int[toBehanded];
            for (int i = 0; i < toBehanded; i++)
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

        public static int getValue(int id)
        {
            return Deck[id-1].value;
        }

        public static Card getCard(int id)
        {
            Card tmp = Deck[id - 1];
            return tmp;
        }

        /// <summary>
        /// returns the id of the chosen card, returns 0 if parameters are not valid
        /// </summary>
        /// <param name="value"></param>
        /// <param name="colour"></param>
        /// <returns></returns>
        public static int getCardid(int value, string colour)
        {

            for (int i=0;i<Deck.Count; i++ )
            {
                if (Deck[i].value == value && Deck[i].getColour()==colour)
                {
                    return Deck[i].id;
                }
               

            }
            return 0;
        }

       
    }

   
}
