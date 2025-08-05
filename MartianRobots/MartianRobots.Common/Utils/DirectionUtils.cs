using MartianRobots.Common.Enums;

namespace MartianRobots.Common.Utils;

public static class DirectionUtils
{
    public static readonly Direction[] OrderedDirections =
    {
        Direction.North,
        Direction.East,
        Direction.South,
        Direction.West
    };
    
    public const int DirectionCount = 4;
    public const int LeftTurnOffset = -1;
    public const int RightTurnOffset = 1;
    
    public static int NormalizeDirectionIndex(int index) => (index + DirectionCount) % DirectionCount;
}