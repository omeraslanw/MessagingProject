using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessagingProject
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-BOV3B8B\SQLEXPRESS;Initial Catalog=MesajlasmaProjesi;Integrated Security=True");
        public string number;
        void outbox()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select AD+' '+SOYAD as 'ALICI',BASLIK,ICERIK from TblMesajlar inner join TblKisiler on TblMesajlar.ALICI=TblKisiler.NUMARA where GONDEREN=@p1", conn);
                cmd.Parameters.AddWithValue("@p1", number);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
        void inbox()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select AD+' '+SOYAD as 'GÖNDEREN',BASLIK,ICERIK from TblMesajlar inner join TblKisiler on TblMesajlar.GONDEREN=TblKisiler.NUMARA where ALICI=@p1", conn);
                cmd.Parameters.AddWithValue("@p1", number);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            inbox();
            outbox();
            try
            {
                lblNumara.Text = number;
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from TblKisiler where NUMARA=@p1", conn);
                cmd.Parameters.AddWithValue("@p1", number);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblAdSoyad.Text = reader[1] + " " + reader[2];
                }
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into TblMesajlar (GONDEREN,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)", conn);
                cmd.Parameters.AddWithValue("@p1", lblNumara.Text);
                cmd.Parameters.AddWithValue("@p2", mskAlici.Text);
                cmd.Parameters.AddWithValue("@p3", txtBaslik.Text);
                cmd.Parameters.AddWithValue("@p4", rchTxtIcerik.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("The message has been sent!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                inbox();
                outbox();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
