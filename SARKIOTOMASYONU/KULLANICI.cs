using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SARKIOTOMASYONU
{
    [Table("KULLANİCİ")]
    internal class KULLANICI
    {
        [Key]
        public int Kullanici_id { get; set; }

        [Required, MaxLength(50)]
        public string Kullanici_ad { get; set; }

        [Required, MaxLength(50)]
        public string Kullanici_soyad { get; set; }

        [Required, MaxLength(50)]
        public string Kullanici_mail { get; set; }

        [Required, MaxLength(50)]
        public string Kullanici_telefon { get; set; }

        public virtual ICollection<FAVORİLER> FAVORİLERs { get; set; }
    }
}
