﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArschlochHost
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
                if (cardcolour == "herz")
                {

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

        public static void resetDeck()
        {
            for (int i = 0; i < Deck.Count; i++)
            {
                Deck[i].handedOut = false;
            }
        }

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
            return Deck[id].value;
        }

       
    }

   
}
