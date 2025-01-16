using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        private string kullaniciAdi;

        public Form4(string kullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciAdi;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.SetFormRound();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(kullaniciAdi);
            form5.Show();
            this.Hide();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form form = new Form6(kullaniciAdi);
            form.Show();
            this.Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Form form = new Form7(kullaniciAdi);
            form.Show();
            this.Close();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(kullaniciAdi);
            form3.Show();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

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
