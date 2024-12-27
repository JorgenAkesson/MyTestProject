using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BillingAPI.Models;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public DbSet<Billing> Billings { get; set; } = null!;
}