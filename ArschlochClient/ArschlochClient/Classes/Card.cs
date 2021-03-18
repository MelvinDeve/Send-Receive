using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
//using System.Windows.Controls;

namespace ArschlochClient
{
    public class Card
    {
        public int value;
        private string colour;
        private static int counter = 0;

        public int id { get; set; }
        public bool handedOut;


        public Card(int value, string colour)
        {
            this.colour = colour;
            this.value = value;
            this.id = System.Threading.Interlocked.Increment(ref counter);
            handedOut = false;

        }
        public string getColour () {
            return colour;
        }

        

    }
}
