using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArschlochHost
{
    class Logic
    {
      public bool checkTurn(int onthetable, int onHand, int numOT,player currentplayer)
        {
            
            int valOT = deck.getValue(onthetable);
            int valOH = deck.getValue(onHand);
            int numOH = checkamount(valOT, currentplayer);


            if (valOH>valOT && numOH>=numOT)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int checkamount(int value, player currentplayer)
        {
            int count = 0;
            int [] tmpcards = currentplayer.getHandCards();
            for (int i=0; i<tmpcards.Length;i++)
            {
                if (deck.getValue(tmpcards[i])==value)
                {
                    count++;
                }
            }
            return count;
        }

        public void switchCards(player arschloch, player koenig, int acardID, int kcardID)
        {

            for (int i=0; i<arschloch.getHandCards().Length;i++)
            {
                if (arschloch.getHandCards()[i]==acardID)
                {
                    arschloch.getHandCards()[i] = kcardID;
                }
            }
            for(int i=0; i<koenig.getHandCards().Length;i++)
            {
                if(koenig.getHandCards()[i]==kcardID)
                {
                    koenig.getHandCards()[i] = acardID;
                }
            }
        }
    }
}
