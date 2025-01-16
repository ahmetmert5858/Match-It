using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox2.PasswordChar = '*';

        }

        static string constring = "Data Source=DESKTOP-26FG2D2\\MSSQLSERVER01;Initial Catalog=db2;Integrated Security=True;Encrypt=False";
        SqlConnection sqlConnection = new SqlConnection(constring);

        private void Form1_Load(object sender, EventArgs e)
        {
            int cornerRadius = 30; 
            this.Region = new Region(GetRoundedRectangle(this.ClientRectangle, cornerRadius));
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz!");
                return;
            }

            if (!Regex.IsMatch(textBox1.Text, @"^[a-zA-Z]"))
            {
                MessageBox.Show("Kullanıcı adı bir harf ile başlamalıdır ve özel karakter içeremez!");
                return;
            }

            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                string kontrol = @"
            SELECT COUNT(*) 
            FROM users_table 
            WHERE kullanici_adi COLLATE Latin1_General_BIN = @kullanici_adi";
                SqlCommand kontrolKomutu = new SqlCommand(kontrol, sqlConnection);
                kontrolKomutu.Parameters.AddWithValue("@kullanici_adi", textBox1.Text);

                int count = (int)kontrolKomutu.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Bu kullanıcı adı zaten kayıtlı.");
                    sqlConnection.Close();
                    return;
                }

                string kayit = "INSERT INTO users_table(kullanici_adi, sifre) VALUES(@kullanici_adi, @sifre)";
                SqlCommand komut = new SqlCommand(kayit, sqlConnection);
                komut.Parameters.AddWithValue("@kullanici_adi", textBox1.Text);
                komut.Parameters.AddWithValue("@sifre", textBox2.Text);

                komut.ExecuteNonQuery();
                sqlConnection.Close();
                MessageBox.Show("Kayıt başarıyla eklendi!");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata meydana geldi: " + hata.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Oyunu kapatmak istediğinize emin misiniz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); 
            }

        }

    }
}
