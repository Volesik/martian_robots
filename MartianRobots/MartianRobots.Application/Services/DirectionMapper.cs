using MartianRobots.Application.Interfaces;
using MartianRobots.Common.Constants;
using MartianRobots.Common.Enums;

namespace MartianRobots.Application.Services;

public class DirectionMapper : IDirectionMapper
{
    public Direction CharToDirection(char direction) => char.ToUpper(direction) switch
    {
        DirectionConstants.NorthDirection => Direction.North,
        DirectionConstants.EastDirection  => Direction.East,
        DirectionConstants.SouthDirection => Direction.South,
        DirectionConstants.WestDirection  => Direction.West,
        _                                 => throw new ArgumentException($"Invalid direction: {direction}")
    };

    public char DirectionToChar(Direction direction) => direction switch
    {
        Direction.North => DirectionConstants.NorthDirection,
        Direction.East  => DirectionConstants.EastDirection,
        Direction.South => DirectionConstants.SouthDirection,
        Direction.West  => DirectionConstants.WestDirection,
        _               => DirectionConstants.UnknownDirection
    };
}