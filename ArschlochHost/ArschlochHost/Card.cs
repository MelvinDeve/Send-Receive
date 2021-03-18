using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;

namespace ArschlochHost
{
    public class Card
    {
        public int value;
        private string colour;
        private Image img;
        private static int counter = 0;

        public int id { get; set; }
        public bool handedOut;


        public Card(int value, string colour)
        {
            this.colour = colour;
            this.value = value;
            img = setImg();
            this.id = System.Threading.Interlocked.Increment(ref counter);
            handedOut = false;

        }
        public string getColour () {
            return colour;
        }

        private Image setImg()
        {

            //search for file in folder where all pngs sit: eg heart7
            //set img to found file in folder
            string filename = colour + value;

            //Image temp = Image.FromFile(filename);

            Image temp = new Image();
            return temp;

        }

    }
}
