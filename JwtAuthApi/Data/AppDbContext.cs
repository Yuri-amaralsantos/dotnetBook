using JwtAuthApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ReadBook> ReadBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReadBook>()
                .HasOne(rb => rb.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(rb => rb.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReadBook>()
                .HasOne(rb => rb.User)
                .WithMany() // ou .WithMany(u => u.ReadBooks) se você tiver adicionado isso no modelo User
                .HasForeignKey(rb => rb.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
