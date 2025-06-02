using FluentAssertions;
using Products.Common.Infrastructure;
using Products.Common.Entities;

public class InMemoryCrudServiceTests
{
    private string GetTempFilePath() => Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.json");

    [Fact]
    public async Task CreateAsync_ShouldAddElement()
    {
        var service = CreateService();

        var tv = Television.Create("Test TV", 1000, "Test Description", true);
        var result = await service.CreateAsync(tv);

        result.Should().BeTrue();
        (await service.ReadAsync(tv.Id)).Should().Be(tv);
    }

    [Fact]
    public async Task ReadAsync_ShouldReturnElement()
    {
        var service = CreateService();

        var tv = Television.Create();
        await service.CreateAsync(tv);

        var read = await service.ReadAsync(tv.Id);
        read.Should().Be(tv);
    }

    [Fact]
    public async Task ReadAllAsync_ShouldReturnAll()
    {
        var service = CreateService();

        await service.CreateAsync(Television.Create());
        await service.CreateAsync(Television.Create());

        var all = await service.ReadAllAsync();

        all.Should().HaveCount(2);
    }

    [Fact]
    public async Task ReadAllAsync_WithPagination_ShouldReturnCorrectPage()
    {
        var service = CreateService();
        var tvs = Enumerable.Range(1, 20)
            .Select(i => new Television(Guid.NewGuid(), "TV " + i, 0, "...", false))
            .ToList();

        foreach (var p in tvs)
            await service.CreateAsync(p);

        var page = await service.ReadAllAsync(page: 1, amount: 3);
        page.Should().HaveCount(3);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateElement()
    {
        var service = CreateService();

        var tv = Television.Create();
        await service.CreateAsync(tv);

        tv.Name = "After";
        await service.UpdateAsync(tv);

        var updated = await service.ReadAsync(tv.Id);
        updated.Name.Should().Be("After");
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveElement()
    {
        var service = CreateService();

        var tv = Television.Create();
        await service.CreateAsync(tv);

        var result = await service.RemoveAsync(tv);
        result.Should().BeTrue();

        var after = await service.ReadAsync(tv.Id);
        after.Should().BeNull();
    }

    [Fact]
    public async Task SaveAsync_ShouldWriteToFile_AndReloadOnInit()
    {
        var path = GetTempFilePath();
        var service = CreateService();

        var tv = Television.Create("Persisted", 1000, "Test Description", true);
        await service.CreateAsync(tv);
        await service.SaveAsync(path);

        // Створюємо новий інстанс сервісу, щоб перевірити десеріалізацію
        var newService = CreateService();
        await newService.LoadAsync(path);

        newService.Count().Should().BeGreaterThan(0);

        File.Delete(path); // очищення після тесту
    }

    private AsyncCrudService<Television> CreateService()
    {
        return new AsyncCrudService<Television>(p => p.Id);
    }
}

