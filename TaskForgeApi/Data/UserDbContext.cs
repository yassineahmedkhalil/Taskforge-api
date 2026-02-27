using Microsoft.EntityFrameworkCore;
using TaskForgeApi.Entities;

namespace TaskForgeApi.Data
{
  public class UserDbContext(DbContextOptions<UserDbContext> options): DbContext(options)
  {
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(b =>
        {
            b.Property(u => u.Email).IsRequired().HasMaxLength(256);
            b.Property(u => u.Username).IsRequired().HasMaxLength(50);

            b.HasIndex(u => u.Email).IsUnique();
            b.HasIndex(u => u.Username).IsUnique();
        });
    }
    }
}
