using AppCore.Enums;

namespace AppCore.Entities;

public class ParkingGate : EntityBase
{
    public required string Name { get; set; }
    public GateType Type { get; set; }
    public required string Location { get; set; }
    public bool IsOperational { get; set; }

    public ICollection<ParkingSession> Sessions { get; set; } = new List<ParkingSession>();
    public ICollection<CameraCapture> Captures { get; set; } = new List<CameraCapture>();
}

