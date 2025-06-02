using Products.Common.Entities;
using Products.Common.Extensions;
using Products.Common.Infrastructure;

public class Program
{
    public static async Task Main(string[] args)
    {
        var service = new AsyncCrudService<Laptop>(x => x.Id);

        Parallel.For(1, 1000, async x => 
        {
            var laptop = Laptop.Create("Laptop", Random.Shared.Next(10, 100), "...", "Intel Core i7", Random.Shared.Next(2, 16));
            await service.CreateAsync(laptop);
        });

        var entries = await service.ReadAllAsync();

        var min = entries.Min(x => x.Price);
        var max = entries.Max(x => x.Price);
        var average = entries.Average(x => x.Price);

        Console.WriteLine("Min: " + min);
        Console.WriteLine("Max: " + max);
        Console.WriteLine("Average: " + average);

        await service.SaveAsync("laptops.json");
    }
}