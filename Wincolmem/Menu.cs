﻿using Game;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Wincolmem
{
    public partial class Menu : Form
    {
        public static bool mainMenuAnimation;
        private Timer mainMenuTimer = new Timer();
        private static int score = 0;
        private static int width = 337;

        public Menu()
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

        private void StartButton_Click(object sender, EventArgs e)
        {
            HideMainMenu();
            ColMem game = new ColMem();
            score = 0;
            StartLevel(0, game);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartLevel(int level, ColMem game)
        {
            int dimension = game.GetLevel(level).dimension;
            ResizeFormForLevel(dimension);
            CreateLabelForLevel(level);
            CreateLabelForScore();


        }

        private void ResizeFormForLevel(int dimension)
        {
            width = (dimension * 80) + (400 / dimension);
            this.Width = width;
            this.Height = (dimension * 98) + 100;        
        }

        private void CreateLabelForLevel(int level)
        {
            Label levelLabel = new Label();
            levelLabel.Text = "LEVEL " + level.ToString();
            levelLabel.Top = 1;
            levelLabel.Left = 1;
            levelLabel.Font = new Font("Debussy", 16);
            levelLabel.ForeColor = Color.Black;
            levelLabel.AutoSize = true;
            this.Controls.Add(levelLabel);
        }

        private void CreateLabelForScore()
        {
            Label scoreLabel = new Label();
            scoreLabel.Text = score.ToString();
            scoreLabel.Top = 1;
            scoreLabel.Left =  width -30 - (score.ToString().Length * 16); //16 is the font size
            scoreLabel.TextAlign = ContentAlignment.TopRight;
            scoreLabel.Font = new Font("Debussy", 16);
            scoreLabel.ForeColor = Color.Black;
            scoreLabel.AutoSize = true;
            this.Controls.Add(scoreLabel);
        }


        private void ResizeFormForMainMenu()
        {
            width = 337;
            this.Height = 493;
        }

        private void HideMainMenu()
        {
            mainMenuTimer.Stop();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            MemoryLabel.Hide();
            StartButton.Hide();
            HiScoresButton.Hide();
            QuitButton.Hide();
        }

        private void ShowMainMenu()
        {            
            label1.Show();
            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            MemoryLabel.Show();
            StartButton.Show();
            HiScoresButton.Show();
            QuitButton.Show();
            mainMenuTimer.Start();
        }

    }
}