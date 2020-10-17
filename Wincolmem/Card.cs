using System;
using System.Drawing;
using System.Windows.Forms;

namespace Wincolmem
{
    public class Card : PictureBox
    {
        private Color cardColour;
        private Menu form;
        private bool IamClicked = false;

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
            if (form.MoreClicks() && !IamClicked)
            {
                base.OnClick(e);
                form.PlayFX(Properties.Resources.click);
                this.BackColor = cardColour;
                this.Image = null;
                form.NotifyCardClicked(this);
                IamClicked = true;
            }
        }

        public void FlipCard()
        {
            this.Image = Properties.Resources.backSide;
            IamClicked = false;
        }

        public void Matched()
        {
            this.Image = Properties.Resources.okT;
            form.PlayFX(Properties.Resources.success);
        }

        public void NoMatched()
        {
            this.Image = Properties.Resources.failT;
            form.PlayFX(Properties.Resources.failure);
        }

    }
}







