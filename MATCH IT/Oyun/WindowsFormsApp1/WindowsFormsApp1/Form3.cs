using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private string kullaniciAdi;

        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.SetFormRound();


        }

        public Form3(string userName)
        {
            InitializeComponent();
            kullaniciAdi = userName;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.SetFormRound();

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(kullaniciAdi); 
            form4.Show();
            this.Hide();  
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8(kullaniciAdi); 
            form8.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Oyundan Çıkmak İstediğine Emin Misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "Emre Özyıldırım\nTelefon: +90 541 621 35 68\nE-posta: emreyedek10.5@gmail.com\n\n" +
                             "Ahmet Mert Bulut\nTelefon: +90 542 453 8100\nE-posta: ahmetmertbulut0@gmail.com";
            MessageBox.Show(message, "İletişim Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9(kullaniciAdi);
            form9.Show();
            this.Hide();
        }
        private void SetFormRound()
        {

            int borderRadius = 30;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(this.Width - borderRadius - 1, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(this.Width - borderRadius - 1, this.Height - borderRadius - 1, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, this.Height - borderRadius - 1, borderRadius, borderRadius, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }
    }
}
