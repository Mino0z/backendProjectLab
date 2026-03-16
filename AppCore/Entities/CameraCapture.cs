using AppCore.Enums;

namespace AppCore.Entities;

public class CameraCapture : EntityBase
{
    public Guid ParkingGateId { get; set; }
    public required ParkingGate Gate { get; set; }

    public required string GateName { get; set; }
    public required string LicensePlate { get; set; }
    public required string DetectedBrand { get; set; }
    public required string DetectedColor { get; set; }
    public DateTime CapturedAt { get; set; }
    public required string ImagePath { get; set; }
    public CaptureType Type { get; set; }
}

