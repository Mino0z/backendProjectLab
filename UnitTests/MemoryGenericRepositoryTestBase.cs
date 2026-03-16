using AppCore.Entities;
using AppCore.Repositories;
using Infrastructure.Memory;

namespace UnitTests;

public abstract class MemoryGenericRepositoryTestBase
{
    protected IGenericRepositoryAsync<Vehicle> CreateRepository() => new MemoryGenericRepository<Vehicle>();

    protected static Vehicle CreateVehicle(string licensePlate, string brand, string color)
    {
        return new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = licensePlate,
            Brand = brand,
            Color = color
        };
    }
}

