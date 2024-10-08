using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-6H04SC8; initial Catalog = CertificateDb; integrated Security =true; TrustServerCertificate=True; ");
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
