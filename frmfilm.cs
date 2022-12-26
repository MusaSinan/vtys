using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace postgredb
{
    public partial class frmfilm : Form
    {
        public frmfilm()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=dbfilimler; user ID=postgres; password=msa15122002;");
        private void frmfilm_Load(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from filmler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into filmler (filmid,film,cikistarihi,dilid,sure,puan) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(TxtFilmId.Text));
            komut1.Parameters.AddWithValue("@p2", TxtFilm.Text);
            komut1.Parameters.AddWithValue("@p3", TxtTarih.Text);
            komut1.Parameters.AddWithValue("@p4", int.Parse(TxtDilId.Text));
            komut1.Parameters.AddWithValue("@p5", int.Parse(TxtSure.Text));
            komut1.Parameters.AddWithValue("@p6", double.Parse(TxtPuan.Text));
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Film ekleme işlemi başarılı");
        }

        private void TxtFilmId_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("Delete from filmler where filmid=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(TxtFilmId.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Film silme başarılı");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update filmler set film=@p1, cikistarihi=@p2, dilid=@p3, sure=@p5, puan=@p6 where filmid=@p4", baglanti);
            komut3.Parameters.AddWithValue("@p1", TxtFilm.Text);
            komut3.Parameters.AddWithValue("@p2", TxtTarih.Text);
            komut3.Parameters.AddWithValue("@p3", int.Parse(TxtDilId.Text));
            komut3.Parameters.AddWithValue("@p4", int.Parse(TxtFilmId.Text));
            komut3.Parameters.AddWithValue("@p5", int.Parse(TxtSure.Text));
            komut3.Parameters.AddWithValue("@p6", double.Parse(TxtPuan.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("Film güncelleme başarılı");
            baglanti.Close();

        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("select * from filmler where film like '%" + TxtFilm.Text + "%'", baglanti);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut4);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();

        }
    }
}
