namespace AppCore.Entities;

public class ParkingTariff : EntityBase
{
	public required string Name { get; set; }
	public TimeSpan FreeParkingDuration { get; set; }
	public decimal HourlyRate { get; set; }
	public decimal DailyMaxRate { get; set; }
	public bool IsActive { get; set; }

	public ICollection<ParkingSession> Sessions { get; set; } = new List<ParkingSession>();
}

