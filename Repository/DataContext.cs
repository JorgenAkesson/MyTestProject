using Microsoft.EntityFrameworkCore;
using MyTestProject.Models;

namespace MyTestProject.DbContext
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
