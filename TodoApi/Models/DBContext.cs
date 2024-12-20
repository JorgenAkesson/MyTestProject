using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Models;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
}