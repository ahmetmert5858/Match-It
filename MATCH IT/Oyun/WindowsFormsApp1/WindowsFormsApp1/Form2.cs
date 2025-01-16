using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;

            this.SetFormRound();

            textBox3.PasswordChar = '*';
        }


        static string constring = "Data Source=DESKTOP-26FG2D2\\MSSQLSERVER01;Initial Catalog=db2;Integrated Security=True;Encrypt=False";
        SqlConnection sqlConnection = new SqlConnection(constring);

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                string query = @"
            SELECT COUNT(1) 
            FROM users_table 
            WHERE kullanici_adi COLLATE Latin1_General_BIN = @kullanici_adi 
            AND sifre COLLATE Latin1_General_BIN = @sifre";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@kullanici_adi", textBox4.Text);
                command.Parameters.AddWithValue("@sifre", textBox3.Text);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 1)
                {
                    string kullaniciAdi = textBox4.Text;
                    Form3 form3 = new Form3(kullaniciAdi);
                    form3.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata meydana geldi: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
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

        private void Form2_Load_1(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Form2_Load_2(object sender, EventArgs e)
        {

        }
    }
}
