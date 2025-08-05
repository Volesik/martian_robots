using MartianRobots.Common.Enums;
using MartianRobots.Common.Models;

namespace MartianRobots.Abstractions.Domains;

public interface IRover
{
    Coordinates Position { get; }
    
    Direction CurrentDirection { get; }
    
    bool IsRoverLost { get; }
    
    void TurnLeft();
    
    void TurnRight();
    
    void MoveForward();
}