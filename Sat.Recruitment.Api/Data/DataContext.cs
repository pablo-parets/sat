using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Entities;

namespace Sat.Recruitment.Api.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasKey(k => new { k.Name, k.Email});

        }
        public DbSet<User> Users { get; set; }
    }
}
