using AppCore.Entities;
using AppCore.Repositories;

namespace Infrastructure.Memory;

public class MemoryParkingSessionRepository : MemoryGenericRepository<ParkingSession>, IParkingSessionRepository
{
    public MemoryParkingSessionRepository()
    {
        var session1 = new ParkingSession
        {
            Id = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            Vehicle = new Vehicle { Id = Guid.NewGuid(), LicensePlate = "WA12345", Brand = "Toyota", Color = "Red" },
            GateName = "Main Gate",
            EntryTime = DateTime.UtcNow.AddHours(-2),
            IsActive = true
        };
        _data.Add(session1.Id, session1);

        var session2 = new ParkingSession
        {
            Id = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            Vehicle = new Vehicle { Id = Guid.NewGuid(), LicensePlate = "KR54321", Brand = "Honda", Color = "Blue" },
            GateName = "Main Gate",
            EntryTime = DateTime.UtcNow.AddHours(-1),
            IsActive = true
        };
        _data.Add(session2.Id, session2);

        var session3 = new ParkingSession
        {
            Id = Guid.NewGuid(),
            VehicleId = Guid.NewGuid(),
            Vehicle = new Vehicle { Id = Guid.NewGuid(), LicensePlate = "GD98765", Brand = "Ford", Color = "Black" },
            GateName = "Main Gate",
            EntryTime = DateTime.UtcNow.AddHours(-5),
            ExitTime = DateTime.UtcNow.AddHours(-1),
            ParkingFee = 15.50m,
            IsActive = false
        };
        _data.Add(session3.Id, session3);
    }

    public Task<ParkingSession?> FindActiveByLicensePlateAsync(string licensePlate)
    {
        return Task.FromResult(_data.Values.FirstOrDefault(ps => ps.Vehicle?.LicensePlate == licensePlate && ps.ExitTime == null));
    }

    public Task<IEnumerable<ParkingSession>> FindAllActiveAsync()
    {
        return Task.FromResult<IEnumerable<ParkingSession>>(_data.Values.Where(ps => ps.ExitTime == null).ToList());
    }

    public Task<IEnumerable<ParkingSession>> FindHistoryByLicensePlateAsync(string licensePlate)
    {
        return Task.FromResult<IEnumerable<ParkingSession>>(_data.Values.Where(ps => ps.Vehicle?.LicensePlate == licensePlate && ps.ExitTime != null).ToList());
    }
}
