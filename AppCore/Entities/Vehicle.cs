namespace AppCore.Entities;

public class Vehicle : EntityBase
{
	public required string LicensePlate { get; set; }
	public required string Brand { get; set; }
	public required string Color { get; set; }

	public ICollection<ParkingSession> Sessions { get; set; } = new List<ParkingSession>();
}

