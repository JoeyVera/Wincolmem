using Game;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Wincolmem
{
    public partial class Menu : Form
    {
        public static bool mainMenuAnimation;
        private Timer mainMenuTimer = new Timer();
        private Timer onGameTimer = new Timer();
        private static int score = 0;
        private static int width = 337;
        private static Card card1clicked = null;
        private static Card card2clicked = null;
        private bool removeCards = false;
        private static int levelScore;
        private int cardsLeft = -1;
        private int currentLevel = 0;
        private ColMem game;

        public Menu()
        {
            InitializeComponent();
        }
          
        private void Form1_Load(object sender, EventArgs e)
        {
            mainMenuAnimation = true;
            mainMenuTimer.Interval = 800;
            onGameTimer.Interval = 1000;
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
            game = new ColMem();
            score = 0;
            StartLevel(0);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartLevel(int level)
        {
            int dimension = game.GetLevel(level).dimension;
            cardsLeft = dimension * dimension;
            levelScore = game.GetLevel(level).points;
            ResizeFormForLevel(dimension);
            UpdateLabelForLevel(level);
            UpdateLabelForScore();
            InitializeTimeLabel(game.GetLevel(level).timeInSecs);
            placeCards(game.GetLevelMap(level), dimension);
        }

        private void ResizeFormForLevel(int dimension)
        {
            width = (dimension * 80) + (400 / dimension);
            this.Width = width;
            this.Height = (dimension * 98) + 100;        
        }

        private void UpdateLabelForLevel(int level)
        {
            levelLabel.Text = "LEVEL " + level.ToString();
            levelLabel.Top = 1;
            levelLabel.Left = 1;
            levelLabel.Font = new Font("Debussy", 16);
            levelLabel.ForeColor = Color.Black;
            levelLabel.AutoSize = true;
            levelLabel.Visible = true;
        }

        private void UpdateLabelForScore()
        {
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Text = score.ToString();
            scoreLabel.Top = 1;
            scoreLabel.Left =  width -30 - (score.ToString().Length * 16); //16 is the font size
            scoreLabel.TextAlign = ContentAlignment.TopRight;
            scoreLabel.Font = new Font("Debussy", 16);
            scoreLabel.ForeColor = Color.Black;
            scoreLabel.AutoSize = true;
            scoreLabel.Visible = true;
        }

        private void InitializeTimeLabel(int seconds)
        {
            var span = new TimeSpan(0, 0, seconds);
            timeLabel.Text = string.Format("{0}:{1:00}", (int)span.TotalMinutes, span.Seconds);
            timeLabel.Left = (width / 2) - 35;
            timeLabel.Visible = true;
        }

        private void placeCards (Color[,] map, int dimension)
        {
            for(int i = 0; i < dimension; i++)
            {
                for(int j = 0; j < dimension; j++)
                {
                    createCard(i, j, width, dimension, map[i, j]);
                }
            }    
        }

        private void createCard(int posX, int posY, int boardWidth, int dimension, Color colour)
        {
            Card card = new Card(posX, posY, width, dimension, colour, this);
            card.Name = "card" + posX.ToString() + posY.ToString();
            this.Controls.Add(card);
        }

        public void updateScore(bool Success)
        {
            if (Success)
                score = score + levelScore;
            else
                score = score - (levelScore / 4); //Is the selection is wrong there is a 25% of penalty

            Label label = (Label)this.Controls.Find("scoreLabel", true).FirstOrDefault();
            label.Left = width - 30 - (score.ToString().Length * 16);
            label.Text = score.ToString();
        }

        public bool MoreClicks()
        {
            if (card2clicked == null)
                return true;
            else
                return false;
        }

        public void NotifyCardClicked(Card clickedCard)
        {
            if (card1clicked == null)
                card1clicked = clickedCard;
            else
            {
                card2clicked = clickedCard;
                checkIfCardsAreSameColour();
            }
        }

        private void checkIfCardsAreSameColour()
        {
            if (card1clicked.BackColor == card2clicked.BackColor)
            {
                updateScore(true);
                removeCards = true;
                removeMatchedCards(); //TODO: move this to timer tick and add 2 secs of delay.
            }
            else
                updateScore(false);
        }

        private void removeMatchedCards()
        {
            card1clicked.Dispose();
            card1clicked = null;
            card2clicked.Dispose();
            card2clicked = null;
            cardsLeft = cardsLeft - 2;
            if (cardsLeft == 0)
                levelComplete();
        }

        private void levelComplete()
        {
            if (currentLevel < 4)
            {
                currentLevel = currentLevel + 1;
                StartLevel(currentLevel);
            }
            else
                gameEnding();
        }

        private void gameEnding()
        {
            game = null;
            //TODO
            // ending screen
            // back to the main menu
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
