using Game;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media;
using SharpDX.XAudio2;
using SharpDX.Multimedia;
using System.Drawing.Text;
using System.Runtime.InteropServices;

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
        private bool markForRemoval = false;
        private bool showResultBeforeRemove = false;
        private bool wrongChoice = false;
        private bool markForFlipping = false;
        private static int levelScore;
        private int cardsLeft = -1;
        private int currentLevel = 0;
        private ColMem game;
        private int levelSecondsLeft;
        private int levelDimension;
        private int ScreenTimerHold = -1;
        private bool GameEnded = false;
        private bool HiScoreScreenActivated = false;
        private HighScores highscorelist;
        private SoundPlayer music;
        private XAudio2 xaudio2;
        private MasteringVoice masteringVoice;
        PrivateFontCollection pfc;

        public Menu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CustomFont();
            xaudio2 = new XAudio2();
            masteringVoice = new MasteringVoice(xaudio2);
            mainMenuAnimation = true;
            mainMenuTimer.Interval = 800;
            onGameTimer.Interval = 1000;
            highscorelist = new HighScores();
            mainMenuTimer.Start();
            PlayHome();
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
            PlayFX(Properties.Resources.click);
            HideMainMenu();
            game = new ColMem();
            score = 0;
            GameEnded = false;
            StartLevel(0);
            PlayInGame();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            PlayFX(Properties.Resources.click);
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
                markForRemoval = true;
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
            if (currentLevel < game.TotalNumberOfLevels() -1)
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
            cleanBoard();
            ShowEndGameScreeen();
        }

        private void cleanBoard()
        {
            GameEnded = true;
            game = null;
            GetRidOfInGameStuff();
            ScreenTimerHold = 0;
        }

        private void ShowEndGameScreeen()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Properties.Resources.gameEnd;
            this.Height = 370;
            this.Width = 500;

            pictureBox.Height = 370;
            pictureBox.Width = 500;

            pictureBox.Left = 1;
            pictureBox.Top = 1;
            pictureBox.Name = "EndPicture";

            PlayEndGame();

            this.Controls.Add(pictureBox);
        }

        private void onGameTimer_Tick(object sender, System.EventArgs e)
        {            
            if (GameEnded)
            {
                if (ScreenTimerHold >= 0) //Display ending screen for 5 sec.
                {
                    ScreenTimerHold++;
                    if (ScreenTimerHold == 10)
                    {
                        ScreenTimerHold = -1;
                        CloseGame();
                    }
                }
            }
            else
            {
                levelSecondsLeft--;

                if (levelSecondsLeft < 0)
                    gameOver();
                else
                {
                    UpdateTimeLabel();

                    if (markForRemoval)
                        RemoveCards();

                    if (wrongChoice)                    
                        FlipCards();                    
                }
            }
        }

        private void CloseGame()
        {
            onGameTimer.Stop();
            onGameTimer.Enabled = false;
            SaveIfHighScore();
            RemoveEndScreen();
            ResizeFormForMainMenu();
            ShowMainMenu();
        }

        private void FlipCards()
        {
            if (markForFlipping)
            {
                card1clicked.FlipCard();
                card2clicked.FlipCard();
                wrongChoice = false;
                markForFlipping = false;
                markForRemoval = false;
                card1clicked = null;
                card2clicked = null;
            }
            else
            {
                card1clicked.NoMatched();
                card2clicked.NoMatched();
                markForFlipping = true;
            }
        }

        private void RemoveCards()
        {
            if (showResultBeforeRemove)
            {
                removeMatchedCards();
                markForRemoval = false;
                showResultBeforeRemove = false;
            }
            else
            {
                card1clicked.Matched();
                card2clicked.Matched();
                showResultBeforeRemove = true;
            }
        }

        private void gameOver()
        {
            cleanBoard();
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
            pictureBox.Name = "EndPicture";

            Label label = new Label();
            label.Text = "GAME OVER";
            label.Top = 20;
            label.Left = 30;
            label.Font = new Font(pfc.Families[0], 50);
            label.UseCompatibleTextRendering = true;
            label.ForeColor = Color.Black;
            label.AutoSize = true;
            label.Visible = true;
            label.BackColor = Color.White;
            label.Name = "GameOverLabel";

            this.Controls.Add(pictureBox);
            this.Controls.Add(label);
            label.BringToFront();
            pictureBox.SendToBack();

            PlayGameOver();
        }

        private void RemoveEndScreen()
        {
            Label label = (Label)this.Controls.Find("GameOverLabel" , true).FirstOrDefault();
            if (label != null)
            {
                label.Dispose();
                label = null;
            }
            PictureBox pictureBox = (PictureBox)this.Controls.Find("EndPicture", true).FirstOrDefault();
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
            HiScoreScreenActivated = false;
            pointsText.Visible = false;
            nameText.Visible = false;
            label4.Text = "o";
            label3.Text = "l";
            label2.Text = "o";
            label5.Text = "u";
            MemoryLabel.Text = "Memory";
            MemoryLabel.Left = 38;
            MemoryLabel.Top = 88;
            label1.Show();
            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            MemoryLabel.Show();
            StartButton.Show();
            HiScoresButton.Text = "Hi-Score";
            HiScoresButton.Show();
            QuitButton.Show();
            mainMenuTimer.Start();
            PlayHome();
        }
    
        private void ShowHighScores()
        {
            StartButton.Hide();
            HiScoresButton.Text = "Return";
            HiScoreScreenActivated = true;
            label1.Hide();
            label6.Hide();
            label4.Text = "H";
            label3.Text = "i";
            label2.Text = "g";
            label5.Text = "h";
            MemoryLabel.Left = 60;
            label2.BringToFront();
            MemoryLabel.SendToBack();
            MemoryLabel.Text = "Scores";

            pointsText.Text = "";
            nameText.Text = "";

            foreach (HighScore hs in highscorelist)
            {
                pointsText.Text = pointsText.Text + hs.points.ToString() + "\r\n";
                nameText.Text = nameText.Text + hs.name.ToString().ToUpper() + "\r\n";
            }            

            pointsText.Visible = true;
            nameText.Visible = true;
        }

        private void HiScoresButton_Click(object sender, EventArgs e)
        {
            PlayFX(Properties.Resources.click);
            if (HiScoreScreenActivated)
                ShowMainMenu();
            else
                ShowHighScores();
        }

        private void SaveIfHighScore()
        {
            if (score > highscorelist.GetLowerScore())
            {
                string nameInserted = Prompt.ShowDialog("PLEASE ENTER YOUR NAME", "GOT A HIGH SCORE!!!");
                highscorelist.AddHighScore(new HighScore { points = score, name = nameInserted });
                highscorelist.SaveHighScores();
            }
        }

        private void PlayHome()
        {
            System.IO.Stream str = Properties.Resources.home;
            music = new SoundPlayer(str);
            music.PlayLooping();
        }

        private void PlayInGame()
        {
            System.IO.Stream str = Properties.Resources.inGameMusic;
            music = new SoundPlayer(str);
            music.PlayLooping();
        }

        private void PlayEndGame()
        {
            System.IO.Stream str = Properties.Resources.endGameMusic;
            music = new SoundPlayer(str);
            music.Play();
        }

        private void PlayGameOver()
        {
            System.IO.Stream str = Properties.Resources.gameOverMusic;
            music = new SoundPlayer(str);
            music.Play();
        }

        public void PlayFX(System.IO.Stream resource)
        {
            
            var stream = new SoundStream(resource);
            var waveFormat = stream.Format;
            var buffer = new AudioBuffer
            {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream
            };
            stream.Close();

            var sourceVoice = new SourceVoice(xaudio2, waveFormat, true);
            sourceVoice.SubmitSourceBuffer(buffer, stream.DecodedPacketsInfo);
            sourceVoice.Start();

        }

        //borrowed from stackoverflow
        private void CustomFont()
        {
            //Create your private font collection object.
            pfc = new PrivateFontCollection();

            //Select your font from the resources.
            //My font here is "Digireu.ttf"
            int fontLength = Properties.Resources.debussy.Length;

            // create a buffer to read in to
            byte[] fontdata = Properties.Resources.debussy;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            // pass the font to the font collection
            pfc.AddMemoryFont(data, fontLength);

            MemoryLabel.Font = new Font(pfc.Families[0], 39.75F);
            StartButton.Font = new Font(pfc.Families[0], 15F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            HiScoresButton.Font = new Font(pfc.Families[0], 15F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            QuitButton.Font = new Font(pfc.Families[0], 15F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label1.Font = new Font(pfc.Families[0], 39.75F,FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label3.Font = new Font(pfc.Families[0], 39.75F);
            label4.Font = new Font(pfc.Families[0], 39.75F);
            label5.Font = new Font(pfc.Families[0], 39.75F);
            label6.Font = new Font(pfc.Families[0], 39.75F);
            timeLabel.Font = new Font(pfc.Families[0], 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            levelLabel.Font = new Font(pfc.Families[0], 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            scoreLabel.Font = new Font(pfc.Families[0], 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            label2.Font = new Font(pfc.Families[0], 39.75F);
            pointsText.Font = new Font(pfc.Families[0], 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));           
            nameText.Font = new Font(pfc.Families[0], 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        }
    }

    //Dialog window to insert high score
    //borrowed from stack overflow
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }

}
