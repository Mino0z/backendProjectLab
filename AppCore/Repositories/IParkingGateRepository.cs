using AppCore.Entities;

namespace AppCore.Repositories;

public interface IParkingGateRepository : IGenericRepositoryAsync<ParkingGate>
{
    Task<ParkingGate?> FindByNameAsync(string name);
}
