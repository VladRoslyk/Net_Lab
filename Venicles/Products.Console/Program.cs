using Microsoft.EntityFrameworkCore;
using Products.Common.Entities;
using Products.Common.Extensions;
using Products.Common.Infrastructure;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;
using Products.Infrastructure.Repositories;

public class Program
{
    public static async Task Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            //.UseSqlServer("Server=DESKTOP-QUOHQAA\\SQLEXPRESS;Database=productsdb;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;")
            .UseMongoDB("mongodb+srv://user:0Password1@cluster0.fuvwccg.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0", "products")
            .Options;

        var context = new ProductsDbContext(options);

        var television = new TelevisionModel()
        {
            Description = "High-quality 4K TV",
            Id = Guid.NewGuid(),
            Name = "Samsung QLED",
            Price = 2000,
            HasQualityDisplay = true
        };

        context.Database.EnsureCreated();

        var repository = new TelevisionRepository(context);
        var service = new TelevisionService(repository);

        await service.CreateAsync(television);

        var found = await service.ReadAsync(television.Id);

        if (found is not null)
        {
            Console.WriteLine($"FOUND {found.Name}");
        }

        foreach (var item in await service.ReadAllAsync())
        {
            Console.WriteLine($"TELEVISION: {item.Name} {item.Price}$");
        }

        television.HasQualityDisplay = false;

        if (await service.UpdateAsync(television))
        {
            Console.WriteLine($"UPDATED {television.Name}");
        }

        if (await service.RemoveAsync(television))
        {
            Console.WriteLine($"DELETED {television.Name}");
        }
    }
}
