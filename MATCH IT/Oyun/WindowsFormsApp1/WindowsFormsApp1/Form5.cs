using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {

        // Timer değişkeni tanımlandı.
        private Timer gameTimer;
        // Geri sayım başlangıç süresi (saniye cinsinden).
        private int countDownTime = 40;
        // Hamle sayısı.
        private int moveCount = 0;
        // Oyun skoru.
        private int score = 0;
        // Final skoru.
        private int finalScore = 0;
        // Rastgele sayı üretici.
        private Random rnd = new Random();

        // Oyun ızgarası için sabitler.
        const int GRID_COLUMNS = 4;
        const int GRID_ROWS = 4;
        int cardWidth = 90;
        int cardHeight = 120;
        int padding = 20;

        // Oyun durumu ve kart seçim durumları.
        bool IsGameOver = false;
        bool CanSelect = true;
        int FindeableCardCount = GRID_COLUMNS * GRID_ROWS / 2;
        int FindedCardCount;

        // Seçilen kartlara ait tag'ler.
        string firstCardTag;
        string secondCardTag;
        CardClass FirstCard;
        CardClass SecondCard;

        // Tahta ve kart listesi.
        CardClass[,] Board = new CardClass[GRID_ROWS, GRID_COLUMNS];
        List<CardClass> Cards = new List<CardClass>();
        string kullaniciAdi;

        // Oyun tahtası paneli.
        private Panel DrawBoard;

        public Form5(string userName)
        {
            InitializeComponent();
            kullaniciAdi = userName;  // Kullanıcı adı parametre olarak alınır.
            InitializeDrawBoard();    // Tahta çizimini başlat.
            InitializeGameTimer();    // Oyun zamanlayıcısını başlat.
            UpdateCards();            // Kartları günceller.
            LoadBoard();              // Kartları tahtaya yükler.
            TimeTxt.Text = countDownTime.ToString(); // Zamanı gösterir.
            Label3.Text = moveCount.ToString(); // Hamle sayısını gösterir.
            label5.Text = score.ToString(); // Puanı gösterir.
            gameTimer.Start();         // Zamanlayıcıyı başlatır.
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        // Tahta için gerekli çizim işlemleri yapılır.
        private void InitializeDrawBoard()
        {
            DrawBoard = new Panel
            {
                Width = GRID_COLUMNS * (cardWidth + padding) + padding,
                Height = GRID_ROWS * (cardHeight + padding) + padding,
                BackColor = Color.White,
                Location = new Point(10, 10)
            };
            DrawBoard.Paint += DrawBoard_Paint;  // Çizim işlemi için Paint eventi eklenir.
            this.Controls.Add(DrawBoard);       // Paneller formun içine eklenir.
        }

        // Çizim olayını ele alır ve tahtanın çizilmesini sağlar.
        private void DrawBoard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);  // Kartlar arası sınırları çizen kalem.

            int offsetX = 2;
            int offsetY = 2;

            // Yatay çizgileri çizer
            for (int i = 0; i <= GRID_ROWS; i++)
            {
                int y = i * (cardHeight + padding) + offsetY;
                g.DrawLine(pen, offsetX, y, DrawBoard.Width - padding / 1, y);
            }

            // Dikey çizgileri çizer
            for (int j = 0; j <= GRID_COLUMNS; j++)
            {
                int x = j * (cardWidth + padding) + offsetX;
                g.DrawLine(pen, x, offsetY, x, DrawBoard.Height - padding / 1);
            }
        }

        // Zamanlayıcıyı başlatır ve her saniye için tetiklenen olay ekler.
        private void InitializeGameTimer()
        {
            gameTimer = new Timer();
            gameTimer.Interval = 1000;  // Zamanlayıcı 1 saniyede bir tetiklenir.
            gameTimer.Tick += TimerEvent;  // TimerEvent metodu her saniyede çağrılır.
        }

        // Kartları rastgele seçer ve günceller.
        public void UpdateCards()
        {
            HashSet<int> selectedImages = new HashSet<int>();

            // Yeterli sayıda eşleşen kart resmi seçilene kadar döner
            while (selectedImages.Count < GRID_COLUMNS * GRID_ROWS / 2)
            {
                int randomImage = rnd.Next(0, 33);  // 0 ile 32 arasında rastgele bir kart resmi seçilir.
                if (!selectedImages.Contains(randomImage))
                {
                    selectedImages.Add(randomImage);  // Aynı resmi tekrar eklememek için kontrol.
                }
            }

            // Seçilen resimler kartlara atanır ve kartlar listesine eklenir.
            foreach (int imageID in selectedImages)
            {
                Image cardImage = (Image)Properties.Resources.ResourceManager.GetObject($"_{imageID}");
                if (cardImage == null)
                {
                    MessageBox.Show($"Resource '{imageID}' bulunamadı. Lütfen kontrol edin.");
                    return;
                }

                Image backgroundImage = Properties.Resources.görsel;  // Kartın arka plan görseli.

                for (int i = 0; i < 2; i++) // Her resim 2 kez kartta olacak şekilde eklenir.
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

                    newCard.Click += Card_Click; // Kartın tıklanma olayını bağlar.

                    Cards.Add(newCard); // Kartları listeye ekler.
                }
            }
        }

        // Kartları tahtada rastgele yerleştirir.
        public void LoadBoard()
        {
            Shuffle(Cards);  // Kartları karıştırır.

            // Kartları tahtada uygun yerlere yerleştirir.
            for (int i = 0; i < GRID_ROWS; i++)
            {
                for (int j = 0; j < GRID_COLUMNS; j++)
                {
                    int index = i * GRID_COLUMNS + j;
                    if (index >= Cards.Count)
                        break;

                    Board[i, j] = Cards[index];

                    Board[i, j].pictureBox.Left = j * (cardWidth + padding) + padding / 2;
                    Board[i, j].pictureBox.Top = i * (cardHeight + padding) + padding / 2;

                    DrawBoard.Controls.Add(Board[i, j].pictureBox);
                }
            }
        }

        // Kartları karıştırmak için kullanılan genel bir metot.
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

        // Kart tıklama olayını ele alır.
        private void Card_Click(object sender, EventArgs e)
        {
            if (CanSelect)
            {
                if (IsGameOver)
                {
                    return; // Oyun bittiğinde başka bir işlem yapılmaz.
                }

                // İlk kart seçildiğinde
                if (firstCardTag == null)
                {
                    FirstCard = sender as CardClass;
                    if (FirstCard.pictureBox.Tag != null && FirstCard.pictureBox.Image == null)
                    {
                        FirstCard.ShowCard();  // Kart gösterilir.
                        firstCardTag = FirstCard.pictureBox.Tag.ToString();
                        FirstCard.pictureBox.BackgroundImage = null;
                    }
                }
                // İkinci kart seçildiğinde
                else if (secondCardTag == null)
                {
                    SecondCard = sender as CardClass;
                    if (SecondCard.pictureBox.Tag != null && SecondCard.pictureBox.Image == null)
                    {
                        SecondCard.ShowCard();  // Kart gösterilir.
                        secondCardTag = SecondCard.pictureBox.Tag.ToString();
                        SecondCard.pictureBox.BackgroundImage = null;

                        CanSelect = false;  // Kartlar arasında seçim yapılmasını engeller.

                        moveCount++;  // Hamle sayısını artırır.
                        Label3.Text = moveCount.ToString();

                        CheckPictures(FirstCard.pictureBox, SecondCard.pictureBox);  // Kartları karşılaştırır.
                    }
                }
            }
        }

        // Seçilen kartları karşılaştırır ve eşleşip eşleşmediğini kontrol eder.
        public void CheckPictures(PictureBox A, PictureBox B)
        {
            Timer timer = new Timer();
            timer.Interval = 1000;  // 1 saniyelik gecikme ekler.
            timer.Tick += (s, e) =>
            {
                if (IsGameOver)
                {
                    timer.Stop();  // Oyun bittiğinde zamanlayıcıyı durdurur.
                    return;
                }

                if (firstCardTag == secondCardTag)
                {
                    FindedCardCount++;  // Eşleşen kart sayısını artırır.

                    A.Visible = false;  // Eşleşen kartları görünmez yapar.
                    B.Visible = false;

                    DrawBoard.Controls.Remove(A);  // Tahtadan çıkarır.
                    DrawBoard.Controls.Remove(B);

                    score += 10;  // Puanı artırır.
                    label5.Text = score.ToString();

                    if (FindedCardCount >= FindeableCardCount)
                    {
                        Win();  // Oyun biterse kazandığını bildirir.
                    }
                }
                else
                {
                    A.Image = null;  // Eşleşmeyen kartları tekrar sıfırlar.
                    B.Image = null;
                    A.BackgroundImage = Properties.Resources.görsel;
                    B.BackgroundImage = Properties.Resources.görsel;

                    FirstCard.UnvisibleCard();  // Kartları tekrar gizler.
                    SecondCard.UnvisibleCard();

                    if (moveCount > 1)
                    {
                        score -= 2;  // Eşleşmeyen kartlardan sonra puan düşer.
                        label5.Text = score.ToString();
                    }
                }

                firstCardTag = null;
                secondCardTag = null;
                CanSelect = true;  // Seçim yapılabilir hale gelir.
                timer.Stop();  // Timer'ı durdurur.
            };
            timer.Start();  // Timer'ı başlatır.
        }

        // Oyunun kazandığında yapılacak işlemleri tanımlar.
        public void Win()
        {
            if (!IsGameOver)
            {
                IsGameOver = true;
                gameTimer.Stop();

                finalScore = score;

                SaveScore(kullaniciAdi, finalScore);  // Puanı kaydeder.

                MessageBox.Show($"KAZANDINIZ!!! Puanınız: {finalScore}");

                Form3 form3 = new Form3(kullaniciAdi);
                form3.Show();
                this.Hide();
            }
        }

        // Puanı veritabanına kaydeder.
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

                    command.ExecuteNonQuery();  // SQL komutunu çalıştırır.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
        }

        // Zamanlayıcı olayı, her saniye çalışır.
        private void TimerEvent(object sender, EventArgs e)
        {
            countDownTime--;
            TimeTxt.Text = countDownTime.ToString();  // Kalan zamanı günceller.

            if (countDownTime <= 0 && !IsGameOver)
            {
                IsGameOver = true;
                CanSelect = false;
                gameTimer.Stop();

                score -= 25;  // Zaman dolarsa puan kaybedilir.
                label5.Text = score.ToString();

                finalScore = score;

                SaveScore(kullaniciAdi, finalScore);

                MessageBox.Show($"ZAMAN BİTTİ! Puanınız: {finalScore}");

                Form3 form3 = new Form3(kullaniciAdi);
                form3.Show();
                this.Hide();
            }
        }

        // Oyunu durdurur.
        public void StopGame()
        {
            if (!IsGameOver)
            {
                IsGameOver = true;
                gameTimer.Stop();
            }
        }

        // Başlatma butonuna tıklandığında yapılacak işlem.
        private void button1_Click(object sender, EventArgs e)
        {
            StopGame();
            Form3 form3 = new Form3(kullaniciAdi);
            form3.Show();
            this.Hide();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
