namespace MartianRobots.Abstractions.Utils;

public interface IInstructionValidator
{
    void ValidateCoordinates(int xCoordinate, int yCoordinate);
    void ValidateInstructionLength(string instructions);
}