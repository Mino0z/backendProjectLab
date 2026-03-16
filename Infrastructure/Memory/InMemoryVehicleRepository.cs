using AppCore.Entities;
using AppCore.Repositories;

namespace Infrastructure.Memory;

public class InMemoryVehicleRepository : MemoryGenericRepository<Vehicle>, IVehicleRepository
{
    public Task<Vehicle?> FindByLicensePlateAsync(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            return Task.FromResult<Vehicle?>(null);
        }

        var normalized = NormalizePlate(licensePlate);
        var vehicle = _data.Values.FirstOrDefault(v => NormalizePlate(v.LicensePlate) == normalized);

        return Task.FromResult(vehicle);
    }

    private static string NormalizePlate(string plate)
    {
        return plate.Replace(" ", string.Empty).ToUpperInvariant();
    }
}

