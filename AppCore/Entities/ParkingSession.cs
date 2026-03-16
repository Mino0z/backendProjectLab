namespace AppCore.Entities;

public class ParkingSession : EntityBase
{
    public Guid VehicleId { get; set; }
    public required Vehicle Vehicle { get; set; }

    public Guid ParkingTariffId { get; set; }
    public required ParkingTariff Tariff { get; set; }

    public Guid ParkingGateId { get; set; }
    public required ParkingGate Gate { get; set; }

    public required string GateName { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public decimal? ParkingFee { get; set; }
    public bool IsActive { get; set; }
}

