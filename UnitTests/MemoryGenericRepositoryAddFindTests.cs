using AppCore.Repositories;

namespace UnitTests;

public class MemoryGenericRepositoryAddFindTests : MemoryGenericRepositoryTestBase
{
    private readonly IGenericRepositoryAsync<AppCore.Entities.Vehicle> _repo;

    public MemoryGenericRepositoryAddFindTests()
    {
        _repo = CreateRepository();
    }

    [Fact]
    public async Task AddVehicleToRepositoryTestAsync()
    {
        var expected = CreateVehicle("TK 8434Y", "Toyota", "Blue");

        await _repo.AddAsync(expected);

        var actual = await _repo.FindByIdAsync(expected.Id);
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
        Assert.Equal(expected.Id, actual.Id);
    }

    [Fact]
    public async Task AddAsync_WhenIdIsEmpty_AssignsNewGuidAsync()
    {
        var vehicle = CreateVehicle("TK 1111A", "Kia", "White");
        vehicle.Id = Guid.Empty;

        var added = await _repo.AddAsync(vehicle);

        Assert.NotEqual(Guid.Empty, added.Id);
    }

    [Fact]
    public async Task AddAsync_WhenIdAlreadyExists_ThrowsInvalidOperationExceptionAsync()
    {
        var vehicle = CreateVehicle("TK 2222B", "Ford", "Black");
        await _repo.AddAsync(vehicle);

        var duplicate = CreateVehicle("TK 3333C", "Skoda", "Red");
        duplicate.Id = vehicle.Id;

        await Assert.ThrowsAsync<InvalidOperationException>(() => _repo.AddAsync(duplicate));
    }

    [Fact]
    public async Task FindByIdAsync_WhenEntityDoesNotExist_ReturnsNullAsync()
    {
        var result = await _repo.FindByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task FindAllAsync_ReturnsAllItemsAsync()
    {
        var v1 = CreateVehicle("TK 4444D", "Audi", "Gray");
        var v2 = CreateVehicle("TK 5555E", "BMW", "Green");
        await _repo.AddAsync(v1);
        await _repo.AddAsync(v2);

        var all = (await _repo.FindAllAsync()).ToList();

        Assert.Equal(2, all.Count);
        Assert.Contains(all, x => x.Id == v1.Id);
        Assert.Contains(all, x => x.Id == v2.Id);
    }
}



