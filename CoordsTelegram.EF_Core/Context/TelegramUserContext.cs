using CoordsTelegram.EF_Core.Dbo;
using Microsoft.EntityFrameworkCore;

namespace CoordsTelegram.EF_Core.Context
{
    public class TelegramUserContext : DbContext
    {
        public TelegramUserContext(DbContextOptions<TelegramUserContext> options) : base(options)
        {
        }

        public DbSet<TelegramUserDbo> TelegramUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TelegramUsers.db");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TelegramUserDbo>().ToTable("TelegramUsers");
        }
    }
}
