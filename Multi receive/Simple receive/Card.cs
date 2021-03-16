using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;

namespace Simple_receive
{
    class Card
    {
       private int value;
       private string colour;
       private Image img;
      

        public Card(int value, string colour)
        {
            this.colour = colour;
            this.value = value;
            img = setImg();
        
        }

        private Image setImg()
        {
            string filename = colour + value;
            
            Image temp = Image.FromFile(filename);

            //search for file in folder where all pngs sit: eg heart7
            //set img to found file in folder
            return temp;

        }

    }
}
