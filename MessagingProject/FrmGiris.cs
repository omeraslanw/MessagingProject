using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MessagingProject
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-BOV3B8B\SQLEXPRESS;Initial Catalog=MesajlasmaProjesi;Integrated Security=True");


        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select NUMARA=@p1 from TblKisiler where SIFRE=@p2", conn);
                cmd.Parameters.AddWithValue("@p1", mskNumara.Text);
                cmd.Parameters.AddWithValue("@p2", txtSifre.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    this.Hide();
                    FrmAnaSayfa frm = new FrmAnaSayfa();
                    frm.number=mskNumara.Text;
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Please change the password or number you have typed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please change the password or number you have typed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
