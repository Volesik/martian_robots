namespace MartianRobots.Common.Models;

public readonly record struct Coordinates(int X, int Y)
{
    public override string ToString()
    {
        return $"{X} {Y}";
    }
}