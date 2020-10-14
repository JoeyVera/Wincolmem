using System;
using System.Drawing;
using System.Windows.Forms;

namespace Wincolmem
{
    public partial class Form1 : Form
    {
        public static bool mainMenuAnimation;
        private Timer mainMenuTimer = new Timer();

        public Form1()
        {
            InitializeComponent();
        }
          
        private void Form1_Load(object sender, EventArgs e)
        {
            mainMenuAnimation = true;
            mainMenuTimer.Interval = 800;
            mainMenuTimer.Start();
        }
       

        private void mainMenuTimer_Tick(object sender, System.EventArgs e)
        {
            if (mainMenuAnimation)
            {
                Application.DoEvents();
                AnimateColourLabels();
                this.Invalidate();
            }
        }

        private void AnimateColourLabels()
        {
            label1.ForeColor = ChangeColour(label1.ForeColor);
            label2.ForeColor = ChangeColour(label2.ForeColor);
            label3.ForeColor = ChangeColour(label3.ForeColor);
            label4.ForeColor = ChangeColour(label4.ForeColor);
            label5.ForeColor = ChangeColour(label5.ForeColor);
            label6.ForeColor = ChangeColour(label6.ForeColor);
        }

        public static Color ChangeColour(Color currentColour)
        {
            switch (currentColour.Name)
            {
                case "Blue":
                    return Color.Green;
                case "Green":
                    return Color.Purple;
                case "Purple":
                    return Color.Yellow;
                case "Yellow":
                    return Color.Brown;
                case "Brown":
                    return Color.Cyan;
                case "Cyan":
                    return Color.Red;
                default:
                    return Color.Blue;
            }
        }

    }
}
