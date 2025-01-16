using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        private string kullaniciAdi;
        private System.Windows.Forms.Timer gameTimer;
        private int countDownTime = 70;
        private int moveCount = 0;
        private int score = 0;
        private int finalScore = 0; 
        Random rnd = new Random();

        const int GRID_COLUMNS = 6; 
        const int GRID_ROWS = 4;
        int cardWidth = 90;
        int cardHeight = 120;
        int padding = 20;

        bool IsGameOver = false;
        bool CanSelect = true;
        int FindeableCardCount = GRID_COLUMNS * GRID_ROWS / 2;
        int FindedCardCount;

        string firstCardTag;
        string secondCardTag;
        CardClass FirstCard;
        CardClass SecondCard;

        CardClass[,] Board = new CardClass[GRID_ROWS, GRID_COLUMNS];
        List<CardClass> Cards = new List<CardClass>();

        public Form6(string kullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciAdi;
            InitializeGameTimer();
            UpdateCards();
            LoadBoard();
            TimeTxt.Text = countDownTime.ToString();
            Label3.Text = moveCount.ToString();
            label5.Text = score.ToString();
            gameTimer.Start();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void InitializeGameTimer()
        {
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += TimerEvent;
        }

        public void UpdateCards()
        {
            HashSet<int> selectedImages = new HashSet<int>();

            while (selectedImages.Count < GRID_COLUMNS * GRID_ROWS / 2)
            {
                int randomImage = rnd.Next(0, 33);
                if (!selectedImages.Contains(randomImage))
                {
                    selectedImages.Add(randomImage);
                }
            }

            foreach (int imageID in selectedImages)
            {
                Image cardImage = (Image)Properties.Resources.ResourceManager.GetObject($"_{imageID}");
                if (cardImage == null)
                {
                    MessageBox.Show($"Resource '{imageID}' bulunamadı. Lütfen kontrol edin.");
                    return;
                }

                Image backgroundImage = Properties.Resources.görsel;

                for (int i = 0; i < 2; i++)
                {
                    CardClass newCard = new CardClass();
                    PictureBox newPic = new PictureBox();

                    newPic.Image = cardImage;
                    newPic.Height = cardHeight;
                    newPic.Width = cardWidth;
                    newPic.BackColor = Color.White;
                    newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                    newPic.Tag = imageID;
                    newCard.CardPicture(newPic);
                    newCard.ID = imageID;

                    newPic.BackgroundImage = backgroundImage;
                    newPic.BackgroundImageLayout = ImageLayout.Stretch;

                    newCard.Click += Card_Click;

                    Cards.Add(newCard);
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen gridPen = new Pen(Color.Black, 2))
            {
                int horizontalExtension = 10; 
                int verticalExtension = 10;   

                for (int i = 0; i <= GRID_ROWS; i++)
                {
                    int y = i * (cardHeight + padding) + padding / 2;

                    e.Graphics.DrawLine(
                        gridPen,
                        padding / 1 - horizontalExtension, 
                        y,
                        GRID_COLUMNS * (cardWidth + padding) + horizontalExtension, 
                        y
                    );
                }

                for (int j = 0; j <= GRID_COLUMNS; j++)
                {
                    int x = j * (cardWidth + padding) + padding / 2;

                    e.Graphics.DrawLine(
                        gridPen,
                        x,
                        padding / 1 - verticalExtension, 
                        x,
                        GRID_ROWS * (cardHeight + padding) + verticalExtension
                    );
                }
            }
        }



        public void LoadBoard()
        {
            Shuffle(Cards);

            for (int i = 0; i < GRID_ROWS; i++)
            {
                for (int j = 0; j < GRID_COLUMNS; j++)
                {
                    int index = i * GRID_COLUMNS + j;
                    if (index >= Cards.Count)
                        break;

                    Board[i, j] = Cards[index];

                    Board[i, j].pictureBox.Left = j * (cardWidth + padding) + padding;
                    Board[i, j].pictureBox.Top = i * (cardHeight + padding) + padding;

                    this.Controls.Add(Board[i, j].pictureBox);
                }
            }
        }

        private void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (CanSelect)
            {
                if (IsGameOver)
                {
                    return;
                }

                if (firstCardTag == null)
                {
                    FirstCard = sender as CardClass;
                    if (FirstCard.pictureBox.Tag != null && FirstCard.pictureBox.Image == null)
                    {
                        FirstCard.ShowCard();
                        firstCardTag = FirstCard.pictureBox.Tag.ToString();
                        FirstCard.pictureBox.BackgroundImage = null;
                    }
                }
                else if (secondCardTag == null)
                {
                    SecondCard = sender as CardClass;
                    if (SecondCard.pictureBox.Tag != null && SecondCard.pictureBox.Image == null)
                    {
                        SecondCard.ShowCard();
                        secondCardTag = SecondCard.pictureBox.Tag.ToString();
                        SecondCard.pictureBox.BackgroundImage = null;

                        CanSelect = false;

                        moveCount++;
                        Label3.Text = moveCount.ToString();

                        CheckPictures(FirstCard.pictureBox, SecondCard.pictureBox);
                    }
                }
            }
        }

        public void CheckPictures(PictureBox A, PictureBox B)
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) =>
            {
                if (IsGameOver)
                {
                    timer.Stop();
                    return;
                }
                if (firstCardTag == secondCardTag)
                {
                    FindedCardCount++;
                    this.Controls.Remove(A);
                    this.Controls.Remove(B);

                    score += 10;
                    label5.Text = score.ToString();

                    if (FindedCardCount >= FindeableCardCount)
                    {
                        Win();
                    }
                }
                else
                {
                    A.Image = null;
                    B.Image = null;
                    A.BackgroundImage = Properties.Resources.görsel;
                    B.BackgroundImage = Properties.Resources.görsel;
                    FirstCard.UnvisibleCard();
                    SecondCard.UnvisibleCard();

                    if (moveCount > 2)
                    {
                        score -= 1;
                        label5.Text = score.ToString();
                    }
                }

                firstCardTag = null;
                secondCardTag = null;
                CanSelect = true;
                timer.Stop();
            };
            timer.Start();
        }

        public void Win()
        {
            if (!IsGameOver)
            {
                IsGameOver = true;
                gameTimer.Stop();

                finalScore = score;

                SaveScore(kullaniciAdi, finalScore);

                MessageBox.Show($"KAZANDINIZ!!! Puanınız: {finalScore}");
                Form3 form3 = new Form3(kullaniciAdi);
                form3.Show();
                this.Hide();
            }
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            countDownTime--;
            TimeTxt.Text = countDownTime.ToString();

            if (countDownTime <= 0 && !IsGameOver)
            {
                IsGameOver = true;
                CanSelect = false;
                gameTimer.Stop();

                score -= 25;
                label5.Text = score.ToString();

                finalScore = score;

                SaveScore(kullaniciAdi, finalScore);

                MessageBox.Show($"ZAMAN BİTTİ! Puanınız: {finalScore}");
                Form3 form3 = new Form3(kullaniciAdi);
                form3.Show();
                this.Hide();
            }
        }

        private void SaveScore(string username, int score)
        {
            string constring = "Data Source=DESKTOP-26FG2D2\\MSSQLSERVER01;Initial Catalog=db2;Integrated Security=True;Encrypt=False";
            string query = "INSERT INTO scores (username, score) VALUES (@username, @score)";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@score", score);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
        }


        private void Form6_Load(object sender, EventArgs e)
        {
        }

        public void StopGame()
        {
            if (!IsGameOver)
            {
                IsGameOver = true;
                gameTimer.Stop();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            StopGame();
            Form3 form3 = new Form3(kullaniciAdi);
            form3.Show();
            this.Hide();
        }

        private void Form6_Load_1(object sender, EventArgs e)
        {

        }
    }
}
