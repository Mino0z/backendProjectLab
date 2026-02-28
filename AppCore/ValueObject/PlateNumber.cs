using AppCore.Exceptions;

namespace AppCore.ValueObject;

public class PlateNumber
{
    public string Value { get; private set; }

    private PlateNumber(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Plate number cannot be null or empty.");
        }

        Value = input;
    }

    public static PlateNumber Of(string input)
    {
        if (input.Length != 8 )
        {
            throw new NotValidPlateNumberException("Not valid Plate Number, it should be 8 characters.");
        }

        return new PlateNumber(input);
    }
}