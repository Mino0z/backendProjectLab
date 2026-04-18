using AppCore.Entities;
using AppCore.Repositories;

namespace Infrastructure.Memory;

public class InMemoryVehicleRepository : MemoryGenericRepository<Vehicle>, IVehicleRepository
{
    public InMemoryVehicleRepository()
    {
        var vehicle1 = new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = "WA12345",
            Brand = "Toyota",
            Color = "Red"
        };
        _data.Add(vehicle1.Id, vehicle1);

        var vehicle2 = new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = "KR54321",
            Brand = "Honda",
            Color = "Blue"
        };
        _data.Add(vehicle2.Id, vehicle2);

        var vehicle3 = new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = "GD98765",
            Brand = "Ford",
            Color = "Black"
        };
        _data.Add(vehicle3.Id, vehicle3);
    }

    public Task<Vehicle?> FindByLicensePlateAsync(string licensePlate)
    {
        var vehicle = _data.Values.FirstOrDefault(v => v.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(vehicle);
    }
}
