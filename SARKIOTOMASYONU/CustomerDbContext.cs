using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace SARKIOTOMASYONU
{
    internal class CustomerDbContext : DbContext
    {
        public CustomerDbContext() : base("name=CustomerDbContext")
        {
        }

        public DbSet<SARKİ> SARKİs { get; set; }
        public DbSet<ALBUM> ALBUMs { get; set; }
        public DbSet<KULLANICI> KULLANICIs { get; set; }
        public DbSet<FAVORİLER> FAVORİLERs { get; set; }
    }
}