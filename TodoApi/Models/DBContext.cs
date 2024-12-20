using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Order>()
        //    .HasOne(e => e.Account)
        //    .WithMany(e => e.Orders)
        //    .HasForeignKey(e => e.AccountId)
        //    .HasPrincipalKey(e => e.Id);
    }
}