﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArschlochHost
{
    class Logic
    {
      public bool checkTurn(int onthetable, int onHand)
        {
            
            int valOT = deck.getValue(onthetable);
            int valOH = deck.getValue(onHand);

            if (valOH>valOT)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
