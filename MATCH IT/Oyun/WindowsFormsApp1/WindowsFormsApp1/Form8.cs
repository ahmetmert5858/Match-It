using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form8 : Form
    {
        private string kullaniciAdi;

        public Form8(string kullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciAdi; 
            LoadScores();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.SetFormRound();

        }

        private void LoadScores()
        {
            string constring = "Data Source=DESKTOP-26FG2D2\\MSSQLSERVER01;Initial Catalog=db2;Integrated Security=True;Encrypt=False";
            string query = @"
        SELECT u.kullanici_adi, SUM(s.score) AS toplam_puan 
        FROM users_table u 
        INNER JOIN scores s 
        ON u.kullanici_adi COLLATE Latin1_General_BIN = s.username COLLATE Latin1_General_BIN 
        GROUP BY u.kullanici_adi 
        ORDER BY toplam_puan DESC";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.BorderStyle = BorderStyle.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(kullaniciAdi);
            form3.Show();
            this.Hide();
        }

        private void Form8_Load(object sender, EventArgs e)
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
