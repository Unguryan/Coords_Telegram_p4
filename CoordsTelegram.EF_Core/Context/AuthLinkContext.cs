using CoordsTelegram.EF_Core.Dbo;
using Microsoft.EntityFrameworkCore;

namespace CoordsTelegram.EF_Core.Context
{
    public class AuthLinkContext : DbContext
    {
        public AuthLinkContext(DbContextOptions<AuthLinkContext> options) : base(options)
        {
        }

        public DbSet<AuthLinkDbo> AuthLinks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AuthLinks.db");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthLinkDbo>().ToTable("AuthLinks");
        }
    }
}
