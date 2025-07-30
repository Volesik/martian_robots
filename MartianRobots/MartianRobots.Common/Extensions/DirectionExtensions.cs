using MartianRobots.Common.Enums;

namespace MartianRobots.Common.Extensions;

public static class DirectionExtensions
{
    public static Direction TurnLeft(this Direction direction)
    {
        var changedDirection = direction switch
        {
            Direction.North => Direction.West,
            Direction.West => Direction.South,
            Direction.South => Direction.East,
            Direction.East => Direction.North,
            _ => direction
        };
        
        return changedDirection;
    }

    public static Direction TurnRight(this Direction direction)
    {
        var changedDirection = direction switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => direction
        };

        return changedDirection;
    }
}