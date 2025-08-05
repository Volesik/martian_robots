using MartianRobots.Abstractions.Utils;
using MartianRobots.Common.Enums;

namespace MartianRobots.Application.Utils;

public class DirectionUtils : IDirectionUtils
{
    private static readonly Direction[] _orderedDirections =
    {
        Direction.North,
        Direction.East,
        Direction.South,
        Direction.West
    };

    public Direction[] OrderedDirections => _orderedDirections;
    
    public int DirectionCount => OrderedDirections.Length;
    public int LeftTurnOffset => -1;
    public int RightTurnOffset => 1;
    
    public int NormalizeDirectionIndex(int index) => (index + DirectionCount) % DirectionCount;
}