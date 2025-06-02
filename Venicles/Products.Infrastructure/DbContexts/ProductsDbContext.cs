using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Models;

namespace Products.Infrastructure.DbContexts;

public sealed class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<LaptopModel> Laptops { get; set; }
    public DbSet<TelevisionModel> Televisions { get; set; }  // додано
    public DbSet<MonitorModel> Monitors { get; set; }        // додано
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<CustomerModel> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Тут можна додати конфігурації моделей, якщо потрібно
    }
}
