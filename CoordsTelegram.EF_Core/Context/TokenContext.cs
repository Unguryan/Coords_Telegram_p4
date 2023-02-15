using CoordsTelegram.EF_Core.Dbo;
using Microsoft.EntityFrameworkCore;

namespace CoordsTelegram.EF_Core.Context
{
    public class TokenContext : DbContext
    {
        public TokenContext(DbContextOptions<TokenContext> options) : base(options)
        {
        }

        public DbSet<TokenDbo> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Tokens.db");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TokenDbo>().ToTable("Tokens");
        }
    }
}
