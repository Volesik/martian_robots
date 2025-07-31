using MartianRobots.Common.Enums;

namespace MartianRobots.Dto.Mappers;

public class DirectionMapper
{
    public static Direction CharToDirection(char dir) => char.ToUpper(dir) switch
    {
        'N' => Direction.North,
        'E' => Direction.East,
        'S' => Direction.South,
        'W' => Direction.West,
        _   => throw new ArgumentException($"Invalid direction: {dir}")
    };

    public static char DirectionToChar(Direction direction) => direction switch
    {
        Direction.North => 'N',
        Direction.East  => 'E',
        Direction.South => 'S',
        Direction.West  => 'W',
        _               => '?'
    };
}