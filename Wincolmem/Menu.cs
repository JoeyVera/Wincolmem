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
        private bool showResultBeforeRemove = false;
        private bool wrongChoice = false;
        private bool flipCards = false;
        private static int levelScore;
        private int cardsLeft = -1;
        private int currentLevel = 0;
        private ColMem game;
        private int levelSecondsLeft;
        private int levelDimension;
        private int GameOverScreenTimerHold = -1;
        private int EndGameScreenTimerHold = -1;
        private bool GameEnded = false;

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
            GameEnded = false;
            StartLevel(0);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartLevel(int level)
        {
            levelDimension = game.GetLevel(level).dimension;
            cardsLeft = levelDimension * levelDimension;
            levelScore = game.GetLevel(level).points;
            ResizeFormForLevel();
            UpdateLabelForLevel(level);
            UpdateLabelForScore();
            levelSecondsLeft = game.GetLevel(level).timeInSecs;
            UpdateTimeLabel();
            placeCards(game.GetLevelMap(level));
            onGameTimer.Enabled = true;
            onGameTimer.Start();
        }

        private void ResizeFormForLevel()
        {
            width = (levelDimension * 80) + (400 / levelDimension);
            this.Width = width;
            this.Height = (levelDimension * 98) + 100;
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
            scoreLabel.Left = width - 30 - (score.ToString().Length * 16); //16 is the font size
            scoreLabel.TextAlign = ContentAlignment.TopRight;
            scoreLabel.Font = new Font("Debussy", 16);
            scoreLabel.ForeColor = Color.Black;
            scoreLabel.AutoSize = true;
            scoreLabel.Visible = true;
        }

        private void UpdateTimeLabel()
        {
            var span = new TimeSpan(0, 0, levelSecondsLeft);
            timeLabel.Text = string.Format("{0}:{1:00}", (int)span.TotalMinutes, span.Seconds);
            timeLabel.Left = (width / 2) - 35;
            if (levelSecondsLeft < 11)
                timeLabel.ForeColor = Color.Red;
            else
                timeLabel.ForeColor = Color.Black;
            timeLabel.Visible = true;
        }

        private void placeCards(Color[,] map)
        {
            for (int i = 0; i < levelDimension; i++)
            {
                for (int j = 0; j < levelDimension; j++)
                {
                    createCard(i, j, width, map[i, j]);
                }
            }
        }

        private void createCard(int posX, int posY, int boardWidth, Color colour)
        {
            Card card = new Card(posX, posY, width, levelDimension, colour, this);
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
            }
            else
            {
                updateScore(false);
                wrongChoice = true;
            }
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
                onGameTimer.Stop();
                StartLevel(currentLevel);
            }
            else
                gameEnding();
        }

        private void gameEnding()
        {
            game = null;
            GetRidOfInGameStuff();
            //TODO
            // ending screen
            // back to the main menu
        }

        private void onGameTimer_Tick(object sender, System.EventArgs e)
        {
            
            if (!GameEnded)
            {
                levelSecondsLeft--;

                if (levelSecondsLeft < 0)
                    gameOver();
                else
                {
                    UpdateTimeLabel();
                    if (removeCards)
                    {
                        if (showResultBeforeRemove)
                        {
                            removeMatchedCards();
                            removeCards = false;
                            showResultBeforeRemove = false;
                        }
                        else
                        {
                            card1clicked.Matched();
                            card2clicked.Matched();
                            showResultBeforeRemove = true;
                        }
                    }

                    if (wrongChoice)
                    {
                        if (flipCards)
                        {
                            card1clicked.FlipCard();
                            card2clicked.FlipCard();
                            wrongChoice = false;
                            flipCards = false;
                            removeCards = false;
                            card1clicked = null;
                            card2clicked = null;
                        }
                        else
                        {
                            card1clicked.NoMatched();
                            card2clicked.NoMatched();
                            flipCards = true;
                        }
                    }
                }
            }

            else
            {
                if (GameOverScreenTimerHold >= 0)
                {
                    GameOverScreenTimerHold++;
                    if (GameOverScreenTimerHold == 5)
                    {
                        GameOverScreenTimerHold = -1;
                        RemoveGameOverScreen();
                        onGameTimer.Stop();
                        ResizeFormForMainMenu();
                        ShowMainMenu();
                    }
                }

                //TODO: Add same rutine for EndGame

            }

        }

        private void gameOver()
        {
            GameEnded = true;
            game = null;
            GetRidOfInGameStuff();
            GameOverScreenTimerHold = 0;
            ShowGameOverScreeen();
        }           

        private void ShowGameOverScreeen()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Properties.Resources.gameOver;
            this.Height = 500;
            this.Width = 500;

            pictureBox.Height = 500;
            pictureBox.Width = 500;
            
            pictureBox.Left = 1;
            pictureBox.Top = 1;
            pictureBox.Name = "GameOverPicture";

            Label label = new Label();
            label.Text = "GAME OVER";
            label.Top = 20;
            label.Left = 30;
            label.Font = new Font("Debussy", 50);
            label.ForeColor = Color.Black;
            label.AutoSize = true;
            label.Visible = true;
            label.BackColor = Color.White;
            label.Name = "GameOverLabel";

            this.Controls.Add(pictureBox);
            this.Controls.Add(label);
            label.BringToFront();
            pictureBox.SendToBack();
        }

        private void RemoveGameOverScreen()
        {
            Label label = (Label)this.Controls.Find("GameOverLabel" , true).FirstOrDefault();
            label.Dispose();
            label = null;

            PictureBox pictureBox = (PictureBox)this.Controls.Find("GameOverPicture", true).FirstOrDefault();
            pictureBox.Dispose();
            pictureBox = null;
        }

        private void GetRidOfInGameStuff()
        {            
            levelLabel.Visible = false;
            scoreLabel.Visible = false;
            timeLabel.Visible = false;
            GetRidOfRemainingCards();
        }

        private void GetRidOfRemainingCards()
        {
            Card remainingCard = null;

            for (int i = (levelDimension) -1; i >= 0; i--)
            {
                for (int j = (levelDimension) - 1; j >= 0; j--)
                {
                    remainingCard = (Card)this.Controls.Find("card" + i.ToString() + j.ToString(), true).FirstOrDefault();
                    if(remainingCard != null)
                    {
                        remainingCard.Dispose();
                        remainingCard = null;
                    }
                }
            }
        }

        private void ResizeFormForMainMenu()
        {
            this.Width = 337;
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
