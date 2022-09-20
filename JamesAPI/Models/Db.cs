using Microsoft.EntityFrameworkCore;

namespace JamesAPI.Models
{
    public class Db: DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=47.74.86.28;port=3306;user=dbuser;password=MI4m481OuUJ1D9KijI921KFMRFHndvNi;database=JamesAuth", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.1.48-mariadb"));
            }

        }
    }
}
