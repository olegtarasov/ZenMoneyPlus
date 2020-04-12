using Microsoft.EntityFrameworkCore;
using ZenMoneyPlus.Models;

namespace ZenMoneyPlus
{
    public class ZenContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TransactionTag> TransactionTags { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tag>()
                .HasOne<Tag>(x => x.ParentTag)
                .WithMany(x => x.ChildrenTags)
                .HasForeignKey(x => x.Parent);

            modelBuilder.Entity<TransactionTag>()
                .HasKey(x => new {x.TagId, x.TransactionId});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);

            options.UseSqlite("Data Source=data.db");
        }
    }
}