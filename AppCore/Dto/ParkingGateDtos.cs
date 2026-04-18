namespace AppCore.Dto;

public record ParkingGateDto(
    Guid Id,
    string Name,
    string Type,
    string Location,
    bool IsOperational
);

public record CreateGateDto(
    string Name,
    string Type,
    string Location
)
{
    public AppCore.Entities.ParkingGate ToEntity()
    {
        return new AppCore.Entities.ParkingGate
        {
            Name = Name,
            Type = Enum.Parse<AppCore.Enums.GateType>(Type),
            Location = Location,
            IsOperational = false
        };
    }
}
