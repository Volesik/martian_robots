using MartianRobots.Common.Constants;

namespace MartianRobots.Common.Validators;

public static class InstructionValidator
{
    public static void ValidateCoordinates(int xCoordinate, int yCoordinate)
    {
        if (xCoordinate < CoordinateConstants.DefaultPosition || yCoordinate < CoordinateConstants.DefaultPosition)
            throw new ArgumentException("Coordinates must not be negative.");

        if (xCoordinate > InstructionConstants.MaxCoordinateLength || yCoordinate > InstructionConstants.MaxCoordinateLength)
            throw new ArgumentException($"Coordinates must not exceed {InstructionConstants.MaxCoordinateLength}.");
    }
    
    public static void ValidateInstructionLength(string instructions)
    {
        if (string.IsNullOrWhiteSpace(instructions))
            throw new ArgumentException("Command sequence cannot be empty.");

        if (instructions.Length > InstructionConstants.MaxInstructionsLength)
            throw new ArgumentException($"Command sequence must be less than {InstructionConstants.MaxInstructionsLength + 1} characters.");
    }
}