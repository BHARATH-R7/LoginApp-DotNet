using LoginServiceWithJWT.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginServiceWithJWT
{
    public class LoginServiceContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=:memory:");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd(); // Set auto-increment

            // Other model configurations...
        }
    }
}
