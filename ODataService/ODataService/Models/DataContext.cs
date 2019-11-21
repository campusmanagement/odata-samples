using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DataContext() : base()
        {
        }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>();
        }
    }
}
