using AppCore.ValueObject;
namespace AppCore.Models;

public class Car
{
    public int id { get; set; }
    public required PlateNumber PlateNumber { get; set; }
    public DateTime entryDate { get; set; }
    public DateTime? exitDate { get; set; }
}