using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Resources;
using System.Xml;

namespace SARKIOTOMASYONU
{
    public partial class FORM_SANATCI : Form
    {
        public FORM_SANATCI()
        {
            InitializeComponent();
        }

        CustomerDbContext db = new CustomerDbContext();

        private void btn_listele_Click(object sender, EventArgs e)
        {
            try
            {
                var list = db.SARKİs
                    .Include(s => s.ALBUM)
                    .Include(s => s.FAVORİLER)
                    .Select(s => new
                    {
                        s.Sarki_id,
                        SarkiAd = s.Sarki_ad,
                        AlbumAd = s.ALBUM.Album_ad,
                        FavoriID = s.FAVORİLER.Select(f => f.Favori_id).FirstOrDefault(),
                        YayinTarihi = s.Yayin_tarihi
                    })
                    .ToList();

                dataGridView1.DataSource = list;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                
                dataGridView1.Columns["Sarki_id"].Visible = false;
                dataGridView1.Columns["FavoriID"].Visible = false;  
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


        private void FORM_SANATCI_Load(object sender, EventArgs e)
        {
            try
            {
                combo_1.DataSource = db.ALBUMs
                    .OrderBy(c => c.Album_ad)
                    .Select(c => new
                    {
                        c.Album_id,
                        FullName = c.Album_ad + " " + c.Cikis_tarihi
                    })
                    .ToList();

                combo_1.DisplayMember = "FullName";
                combo_1.ValueMember = "Album_id";

                
                combo_2.DataSource = db.SARKİs
                    .OrderBy(s => s.Sarki_ad)
                    .Select(s => new
                    {
                        s.Sarki_id,
                        FullName = s.Sarki_ad
                    })
                    .ToList();

                combo_2.DisplayMember = "FullName";
                combo_2.ValueMember = "Sarki_id";

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

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (combo_1.SelectedValue == null || combo_2.SelectedValue == null)
                {
                    MessageBox.Show("LÜTFEN ŞARKI VE ALBÜM SEÇİMİ YAPINIZ.");
                    return;
                }

                int selectedAlbum_id = (int)combo_1.SelectedValue;
                int selectedSarki_id = (int)combo_2.SelectedValue;

                
                var sarki = db.SARKİs.Find(selectedSarki_id);
                if (sarki != null)
                {
                    sarki.Album_id = selectedAlbum_id;
                    db.SaveChanges();

                    MessageBox.Show("KAYIT BAŞARIYLA EKLENDİ.");
                    btn_listele.PerformClick();
                }
                else
                {
                    MessageBox.Show("ŞARKI BULUNAMADI.");
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
                DialogResult sonuc = MessageBox.Show("SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "SİLME ONAYI",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes)
                {
                    if (dataGridView1.CurrentRow != null)
                    {
                        int sarkiId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Sarki_id"].Value);

                        var sarki = db.SARKİs.Find(sarkiId);
                        if (sarki != null)
                        {
                            db.SARKİs.Remove(sarki);
                            db.SaveChanges();

                            MessageBox.Show("KAYIT BAŞARIYLA SİLİNDİ.");
                            btn_listele.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("KAYIT BULUNAMADI.");
                        }
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

        private void rd_1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rd_1.Checked)
                {
                    
                    var result = db.FAVORİLERs
                        .Include(f => f.KULLANICI) 
                        .Include(f => f.SARKİ) 
                        .GroupBy(f => new
                        {
                            f.KULLANICI.Kullanici_id,
                            f.KULLANICI.Kullanici_ad,
                            f.KULLANICI.Kullanici_soyad,
                            f.KULLANICI.Kullanici_mail,
                            f.KULLANICI.Kullanici_telefon
                        })
                        .Select(g => new
                        {
                            FullName = g.Key.Kullanici_ad + " " + g.Key.Kullanici_soyad ,
                                       
                            TotalCount = g.Count()
                        })
                        .OrderByDescending(x => x.TotalCount)
                        .FirstOrDefault();

                    if (result != null)
                    {
                        lbl_1.Text = $"{result.FullName} - {result.TotalCount} dinlenen şarkı";
                    }
                    else
                    {
                        lbl_1.Text = "KULLANIICI BULUNAMADI.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void rd_2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rd_2.Checked)
                {
                    var result = db.FAVORİLERs
                        .GroupBy(f => f.Sarki_id)
                        .Select(g => new
                        {
                            SarkiId = g.Key,
                            TotalFavourites = g.Count()
                        })
                        .OrderByDescending(x => x.TotalFavourites)
                        .FirstOrDefault();

                    if (result != null)
                    {
                        var sarki = db.SARKİs.FirstOrDefault(s => s.Sarki_id == result.SarkiId);
                        if (sarki != null)
                        {
                            lbl_2.Text = $"{sarki.Sarki_ad} {result.TotalFavourites} kez favorilere eklendi.";
                        }
                        else
                        {
                            lbl_2.Text = "ŞARKI BULUNAMADI";
                        }
                    }
                    else
                    {
                        lbl_2.Text = "ŞARKI BULUNAMADI";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        
        

           
        
    }
}
