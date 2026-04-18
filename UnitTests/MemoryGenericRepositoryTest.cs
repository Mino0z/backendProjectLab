using AppCore.Entities;
using AppCore.Repositories;
using Infrastructure.Memory;
using Xunit;

namespace UnitTests;

public class MemoryGenericRepositoryTest
{
    private readonly IGenericRepositoryAsync<Vehicle> _repo;

    public MemoryGenericRepositoryTest()
    {
        _repo = new MemoryGenericRepository<Vehicle>();
    }

    [Fact]
    public async Task AddVehicleToRepositoryTestAsync()
    {
        // Arrange
        var expected = new Vehicle()
        {
            LicensePlate = "TK 8434Y",
            Brand = "Toyota",
            Color = "Red"
        };
        // Act
        await _repo.AddAsync(expected);
        // Assert
        var actual = await _repo.FindByIdAsync(expected.Id);
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
        Assert.Equal(expected.Id, actual?.Id);
    }

    [Fact]
    public async Task FindById_ReturnsNull_ForNonExistingId()
    {
        var found = await _repo.FindByIdAsync(Guid.NewGuid());
        Assert.Null(found);
    }

    [Fact]
    public async Task FindAllAsync_ReturnsAllAddedEntities()
    {
        await _repo.AddAsync(new Vehicle { LicensePlate = "ABC" });
        await _repo.AddAsync(new Vehicle { LicensePlate = "DEF" });
        
        var all = await _repo.FindAllAsync();
        
        Assert.Equal(2, all.Count());
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingEntity()
    {
        var vehicle = await _repo.AddAsync(new Vehicle { LicensePlate = "ABC", Brand = "Audi" });
        
        vehicle.Brand = "BMW";
        await _repo.UpdateAsync(vehicle);
        
        var updated = await _repo.FindByIdAsync(vehicle.Id);
        Assert.NotNull(updated);
        Assert.Equal("BMW", updated.Brand);
    }

    [Fact]
    public async Task UpdateAsync_ThrowsException_ForNonExistingEntity()
    {
        var vehicle = new Vehicle { Id = Guid.NewGuid(), LicensePlate = "X" };
        await Assert.ThrowsAsync<InvalidOperationException>(() => _repo.UpdateAsync(vehicle));
    }

    [Fact]
    public async Task RemoveByIdAsync_RemovesEntity()
    {
        var vehicle = await _repo.AddAsync(new Vehicle { LicensePlate = "A" });
        
        await _repo.RemoveByIdAsync(vehicle.Id);
        
        var found = await _repo.FindByIdAsync(vehicle.Id);
        Assert.Null(found);
    }

    [Fact]
    public async Task RemoveByIdAsync_ThrowsException_ForNonExistingEntity()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() => _repo.RemoveByIdAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task FindPagedAsync_ReturnsCorrectPageAndMetadata()
    {
        // Arrange
        for (int i = 0; i < 25; i++)
        {
            await _repo.AddAsync(new Vehicle { LicensePlate = $"T{i}" });
        }

        // Act
        var result = await _repo.FindPagedAsync(2, 10);

        // Assert
        Assert.Equal(10, result.Items.Count); // Second page should have 10 items
        Assert.Equal(25, result.TotalCount);
        Assert.Equal(2, result.Page);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(3, result.TotalPages);
        Assert.True(result.HasNext);
        Assert.True(result.HasPrevious);
    }
}
