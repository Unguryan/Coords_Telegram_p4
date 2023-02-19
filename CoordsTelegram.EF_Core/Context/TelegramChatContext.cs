using CoordsTelegram.EF_Core.Dbo;
using Microsoft.EntityFrameworkCore;

namespace CoordsTelegram.EF_Core.Context
{
    public class TelegramChatContext : DbContext
    {
        public TelegramChatContext(DbContextOptions<TelegramChatContext> options) : base(options)
        {
        }

        public DbSet<ChannelDbo> Channels { get; set; }
        public DbSet<AdminDbo> Admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TelegramChats.db");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChannelDbo>().ToTable("Channels");
            modelBuilder.Entity<AdminDbo>().ToTable("Admins");
        }
    }
}
