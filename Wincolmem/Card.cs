using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wincolmem
{
    class Card : PictureBox
    {
        private Color cardColour;

        public Card(int posX, int posY, int boardWidth, int dimension, Color colour)
        {
            cardColour = colour;
            this.Width = 77;
            this.Height = 95;
            this.Left = (70 / dimension) + (posX * Width / dimension) + (posX * 80) ;
            this.Top = 30  + (105 * posY);        //+ (3 + posY)
            this.Image = Properties.Resources.backSide;
            this.BackColor = Form.DefaultBackColor;
        }


    }
}







