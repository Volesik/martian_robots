using MartianRobots.Common.Constants;
using MartianRobots.Common.Enums;
using MartianRobots.Common.Extensions;
using MartianRobots.Common.Models;
using MartianRobots.Domain.Interfaces;

namespace MartianRobots.Domain.Entities;

public class Rover : IRover
{
    private readonly Plateau _plateau;
    
    public Coordinates Position { get; private set; }
    
    public Direction CurrentDirection { get; private set; }
    
    public bool IsRoverLost { get; private set; }

    public Rover(
        int initialPositionX,
        int initialPositionY,
        Direction initialDirection,
        Plateau plateau)
    {
        _plateau = plateau ?? throw new ArgumentNullException(nameof(plateau));
        
        var isInitialPositionOutsidePlateau = !_plateau.IsInsidePlateauArea(initialPositionX, initialPositionY);
        if (isInitialPositionOutsidePlateau)
        {
            throw new ArgumentException("Starting position is outside of plateau area.");
        }
        
        Position = new Coordinates(initialPositionX, initialPositionY);
        CurrentDirection = initialDirection;
    }
    
    public void TurnLeft()
    {
        if (!IsRoverLost)
        {
            CurrentDirection = CurrentDirection.TurnLeft();
        }
    }

    public void TurnRight()
    {
        if (!IsRoverLost)
        {
            CurrentDirection = CurrentDirection.TurnRight();
        }
    }

    public void MoveForward()
    {
        if (IsRoverLost)
        {
            return;
        }
        
        var updatedPositionX = Position.X;
        var updatedPositionY = Position.Y;

        switch (CurrentDirection)
        {
            case Direction.North: 
                updatedPositionY += RoverConstants.RoverSpeed;
                break;
            case Direction.South:
                updatedPositionY -= RoverConstants.RoverSpeed;
                break;
            case Direction.East:
                updatedPositionX += RoverConstants.RoverSpeed;
                break;
            case Direction.West:
                updatedPositionX -= RoverConstants.RoverSpeed;
                break;
            default:
                throw new ArgumentException("Unknown direction.");
        }
        
        var isPositionInsidePlateau = _plateau.IsInsidePlateauArea(updatedPositionX, updatedPositionY);
        if (isPositionInsidePlateau)
        {
            Position = new Coordinates(updatedPositionX, updatedPositionY);
        }
        else
        {
            IsRoverLost = true;
        }
    }
}