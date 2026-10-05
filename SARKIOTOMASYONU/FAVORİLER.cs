using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;



namespace SARKIOTOMASYONU
{
    [Table("FAVORİLER")]
    internal class FAVORİLER
    {
        [Key]
        public int Favori_id { get; set; }

        [Required]
        [ForeignKey("KULLANICI")]
        public int Kullanici_id { get; set; }

        [Required]
        [ForeignKey("SARKİ")]
        public int Sarki_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Eklenme_tarihi { get; set; }

        public virtual KULLANICI KULLANICI { get; set; }
        public virtual SARKİ SARKİ { get; set; }
       
      

    }
}
