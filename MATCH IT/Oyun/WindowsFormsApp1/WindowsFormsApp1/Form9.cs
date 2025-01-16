using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form9 : Form
    {
        private string kullaniciAdi;

        public Form9(string kullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciAdi;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.SetFormRound();

        }

        private void Form9_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(kullaniciAdi);
            form3.Show();
            this.Close();
        }

        private void Form9_Load_1(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

