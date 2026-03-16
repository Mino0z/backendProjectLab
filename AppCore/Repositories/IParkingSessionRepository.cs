using AppCore.Entities;

namespace AppCore.Repositories;

public interface IParkingSessionRepository : IGenericRepositoryAsync<ParkingSession>
{
    Task<ParkingSession?> FindByLicensePlateAsync(string licensePlate);
    Task<IEnumerable<ParkingSession>> FindActiveSessionsAsync();
    Task<IEnumerable<ParkingSession>> FindHistoryByLicensePlateAsync(string licensePlate);
}

