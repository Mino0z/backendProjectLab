using AppCore.Repositories;

namespace UnitTests;

public class MemoryGenericRepositoryPagingTests : MemoryGenericRepositoryTestBase
{
    private readonly IGenericRepositoryAsync<AppCore.Entities.Vehicle> _repo;

    public MemoryGenericRepositoryPagingTests()
    {
        _repo = CreateRepository();
    }

    [Fact]
    public async Task FindPagedAsync_ReturnsCorrectPageMetadataAndItemsAsync()
    {
        for (var i = 0; i < 5; i++)
        {
            await _repo.AddAsync(CreateVehicle($"TK 66{i}AA", "VW", "Silver"));
        }

        var page = await _repo.FindPagedAsync(2, 2);

        Assert.Equal(5, page.TotalCount);
        Assert.Equal(2, page.Page);
        Assert.Equal(2, page.PageSize);
        Assert.Equal(3, page.TotalPages);
        Assert.True(page.HasNext);
        Assert.True(page.HasPrevious);
        Assert.Equal(2, page.Items.Count);
    }

    [Fact]
    public async Task FindPagedAsync_WhenArgumentsAreInvalid_ThrowsArgumentOutOfRangeExceptionAsync()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _repo.FindPagedAsync(0, 10));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _repo.FindPagedAsync(1, 0));
    }
}

