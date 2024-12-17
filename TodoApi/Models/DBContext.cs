using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; } = null!;
}