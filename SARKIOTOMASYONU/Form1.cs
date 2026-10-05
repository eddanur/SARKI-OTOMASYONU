using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
namespace SARKIOTOMASYONU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CustomerDbContext db = new CustomerDbContext();

       
        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                var kullanicilar = db.KULLANICIs.ToList();
                dataGridView1.DataSource = kullanicilar;

                dataGridView1.Columns["Kullanici_id"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["FAVORİLERs"].Visible=false;
                
            }
            catch (Exception ex)
            {
                string hataMesaji = ex.Message;
                if (ex.InnerException != null)
                {
                    hataMesaji += "\n" + ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                        hataMesaji += "\n" + ex.InnerException.InnerException.Message;
                }
                MessageBox.Show($"Hata: {hataMesaji}");
            }
        }

       
        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAd.Text) ||
                    string.IsNullOrWhiteSpace(txtSoyad.Text) ||
                    string.IsNullOrWhiteSpace(txtMail.Text) ||
                    string.IsNullOrWhiteSpace(txtTel.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz!");
                    return;
                }

                KULLANICI yeni = new KULLANICI()
                {
                    Kullanici_ad = txtAd.Text,
                    Kullanici_soyad = txtSoyad.Text,
                    Kullanici_mail = txtMail.Text,
                    Kullanici_telefon = txtTel.Text
                };

                db.KULLANICIs.Add(yeni);
                db.SaveChanges();

                MessageBox.Show("Yeni kullanıcı eklendi!");
                btn_listele.PerformClick();
            }
            catch (Exception ex)
            {
                string hataMesaji = ex.Message;
                if (ex.InnerException != null)
                {
                    hataMesaji += "\n" + ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                        hataMesaji += "\n" + ex.InnerException.InnerException.Message;
                }
                MessageBox.Show($"Hata: {hataMesaji}");
            }
        }

       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells["Kullanici_ad"].Value.ToString();
                txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells["Kullanici_soyad"].Value.ToString();
                txtMail.Text = dataGridView1.Rows[e.RowIndex].Cells["Kullanici_mail"].Value.ToString();
                txtTel.Text = dataGridView1.Rows[e.RowIndex].Cells["Kullanici_telefon"].Value.ToString();
            }
        }

       
        private void btn_güncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Kullanici_id"].Value);
                    KULLANICI kullanici = db.KULLANICIs.Find(selectedId);

                    if (kullanici != null)
                    {
                        kullanici.Kullanici_ad = txtAd.Text;
                        kullanici.Kullanici_soyad = txtSoyad.Text;
                        kullanici.Kullanici_mail = txtMail.Text;
                        kullanici.Kullanici_telefon = txtTel.Text;

                        db.SaveChanges();
                        MessageBox.Show("Kayıt güncellendi!");
                        btn_listele.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                string hataMesaji = ex.Message;
                if (ex.InnerException != null)
                {
                    hataMesaji += "\n" + ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                        hataMesaji += "\n" + ex.InnerException.InnerException.Message;
                }
                MessageBox.Show($"Hata: {hataMesaji}");
            }
        }

      
        private void btn_sil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc = MessageBox.Show(
                    "Silmek istediğinizden emin misiniz?", "Silme Onayı",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes && dataGridView1.CurrentRow != null)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Kullanici_id"].Value);
                    KULLANICI silinecek = db.KULLANICIs.Find(selectedId);

                    if (silinecek != null)
                    {
                        db.KULLANICIs.Remove(silinecek);
                        db.SaveChanges();

                        MessageBox.Show("Kullanıcı silindi!");
                        btn_listele.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                string hataMesaji = ex.Message;
                if (ex.InnerException != null)
                {
                    hataMesaji += "\n" + ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                        hataMesaji += "\n" + ex.InnerException.InnerException.Message;
                }
                MessageBox.Show($"Hata: {hataMesaji}");
            }
        }

        private void btn_form_sanat_Click(object sender, EventArgs e)
        {
            FORM_SANATCI form2 = new FORM_SANATCI();
            this.Hide();
            form2.ShowDialog();
            this.Close();

            

            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
}
          