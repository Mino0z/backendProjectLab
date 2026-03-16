using AppCore.Entities;

namespace AppCore.Repositories;

public interface IVehicleRepository : IGenericRepositoryAsync<Vehicle>
{
    Task<Vehicle?> FindByLicensePlateAsync(string licensePlate);
}

