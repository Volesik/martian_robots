using MartianRobots.Common.Enums;

namespace MartianRobots.Abstractions.Utils;

public interface IDirectionUtils
{
    Direction[] OrderedDirections { get; }
    int DirectionCount { get; }
    int LeftTurnOffset { get; }
    int RightTurnOffset { get; }
    
    int NormalizeDirectionIndex(int index);
}