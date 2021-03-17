using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArschlochClient.Classes
{
    class HandOfCards
    {
        int[] ids = null;

        public void getNewHand()
        {
            HandOfCards MyHand = new HandOfCards();
            SimpleReceive Rec = new SimpleReceive();
            List<Card> Hand = new List<Card>();
            deck deck = new deck();

            foreach (int id in ids)
            {

            }
        }
        


    }
}
