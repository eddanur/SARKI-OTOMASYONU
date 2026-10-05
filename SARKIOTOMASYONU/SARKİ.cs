using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SARKIOTOMASYONU
{
    [Table("SARKİ")]
    internal class SARKİ
    {
        [Key]
        public int Sarki_id { get; set; }

        [Required, MaxLength(50)]
        public string Sarki_ad { get; set; }

        [Required]
        public int Album_id { get; set; }

        [Required]
        public DateTime Yayin_tarihi { get; set; } 

        [ForeignKey("Album_id")]
        public virtual ALBUM ALBUM { get; set; }

       
        public virtual ICollection<FAVORİLER> FAVORİLER { get; set; }
   

    }
}
