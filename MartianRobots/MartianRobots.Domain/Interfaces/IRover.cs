using MartianRobots.Common.Enums;
using MartianRobots.Common.Models;

namespace MartianRobots.Domain.Interfaces;

public interface IRover
{
    Coordinates Position { get; }
    
    Direction CurrentDirection { get; }
    
    void TurnLeft();
    
    void TurnRight();
    
    void MoveForward();
}