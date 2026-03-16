using AppCore.Repositories;

namespace UnitTests;

public class MemoryGenericRepositoryMutationTests : MemoryGenericRepositoryTestBase
{
    private readonly IGenericRepositoryAsync<AppCore.Entities.Vehicle> _repo;

    public MemoryGenericRepositoryMutationTests()
    {
        _repo = CreateRepository();
    }

    [Fact]
    public async Task UpdateAsync_WhenEntityExists_UpdatesStoredValueAsync()
    {
        var vehicle = CreateVehicle("TK 7777F", "Opel", "Blue");
        await _repo.AddAsync(vehicle);

        vehicle.Color = "Yellow";
        var updated = await _repo.UpdateAsync(vehicle);
        var fromRepo = await _repo.FindByIdAsync(vehicle.Id);

        Assert.Equal("Yellow", updated.Color);
        Assert.Equal("Yellow", fromRepo?.Color);
    }

    [Fact]
    public async Task UpdateAsync_WhenEntityDoesNotExist_ThrowsKeyNotFoundExceptionAsync()
    {
        var vehicle = CreateVehicle("TK 8888G", "Honda", "White");

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _repo.UpdateAsync(vehicle));
    }

    [Fact]
    public async Task RemoveByIdAsync_WhenEntityExists_RemovesEntityAsync()
    {
        var vehicle = CreateVehicle("TK 9999H", "Mazda", "Black");
        await _repo.AddAsync(vehicle);

        await _repo.RemoveByIdAsync(vehicle.Id);
        var afterRemove = await _repo.FindByIdAsync(vehicle.Id);

        Assert.Null(afterRemove);
    }

    [Fact]
    public async Task RemoveByIdAsync_WhenEntityDoesNotExist_ThrowsKeyNotFoundExceptionAsync()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _repo.RemoveByIdAsync(Guid.NewGuid()));
    }
}

