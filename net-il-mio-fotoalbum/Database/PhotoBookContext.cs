using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models.Db_Models;

namespace net_il_mio_fotoalbum.Database
{
    public class PhotoBookContext:IdentityDbContext<IdentityUser>
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PhotoBookDb;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
