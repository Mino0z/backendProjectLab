using AppCore.Entities;

namespace AppCore.Repositories;

public interface IParkingSessionRepository : IGenericRepositoryAsync<ParkingSession>
{
    Task<ParkingSession?> FindActiveByLicensePlateAsync(string licensePlate);
    Task<IEnumerable<ParkingSession>> FindAllActiveAsync();
    Task<IEnumerable<ParkingSession>> FindHistoryByLicensePlateAsync(string licensePlate);
}
