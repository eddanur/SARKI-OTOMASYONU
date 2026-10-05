using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARKIOTOMASYONU
{
    [Table("ALBUM")]
    internal class ALBUM
    {
        [Key]
        public int Album_id { get; set; }

        [Required, MaxLength(50)]
        public string Album_ad { get; set; }

        [Required]
        public DateTime Cikis_tarihi { get; set; } 

        public virtual ICollection<SARKİ> SARKİs { get; set; } 
    }
}