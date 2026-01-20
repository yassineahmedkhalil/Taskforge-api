using Microsoft.EntityFrameworkCore;
using TaskForgeApi.Entities;

namespace TaskForgeApi.Data
{
  public class UserDbContext(DbContextOptions<UserDbContext> options): DbContext(options)
  {
    public DbSet<User> Users { get; set; }
  }
}
