using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wincolmem
{
    public class Card : PictureBox
    {
        private Color cardColour;
        private Menu form;

        public Card(int posX, int posY, int boardWidth, int dimension, Color colour, Menu injectedForm)
        {
            form = injectedForm;
            cardColour = colour;
            this.Width = 77;
            this.Height = 95;
            this.Left = (70 / dimension) + (posX * Width / dimension) + (posX * 80) ;
            this.Top = 30  + (105 * posY);
            this.Image = Properties.Resources.backSide;
            this.BackColor = Form.DefaultBackColor;
        }

        protected override void OnClick(EventArgs e)
        {
            if (form.MoreClicks())
            {
                base.OnClick(e);
                this.BackColor = cardColour;
                this.Image = null;
                form.NotifyCardClicked(this);



                form.updateScore(true); //TODO: Remove this and apply correctly.
            }
        }

    }
}







