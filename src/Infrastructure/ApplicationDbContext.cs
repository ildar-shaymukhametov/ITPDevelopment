using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() {}

    public ApplicationDbContext(DbContextOptions options) : base(options) {}

    public DbSet<User> Users { get; set; }
}