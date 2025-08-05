using MartianRobots.Abstractions.Domains;
using MartianRobots.Abstractions.Utils;
using MartianRobots.Application.Interfaces;
using MartianRobots.Common.Constants;
using MartianRobots.Common.Enums;
using MartianRobots.Common.Models;

namespace MartianRobots.Domain.Entities;

public class Rover : IRover
{
    private readonly IPlateau _plateau;
    private readonly IDirectionMapper _directionMapper;
    private readonly IDirectionUtils _directionUtils;
    
    public Coordinates Position { get; private set; }
    
    public Direction CurrentDirection { get; private set; }
    
    public bool IsRoverLost { get; private set; }

    public Rover(
        int initialPositionX,
        int initialPositionY,
        Direction initialDirection,
        IPlateau plateau,
        IDirectionMapper directionMapper,
        IDirectionUtils directionUtils)
    {
        _plateau = plateau ?? throw new ArgumentNullException(nameof(plateau));
        _directionMapper = directionMapper ?? throw new ArgumentNullException(nameof(directionMapper));
        _directionUtils = directionUtils ?? throw new ArgumentNullException(nameof(directionUtils));
        
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
        if (IsRoverLost)
        {
            return;
        }
        
        var currentDirectionIndex = Array.IndexOf(_directionUtils.OrderedDirections, CurrentDirection);
        var newIndex = _directionUtils.NormalizeDirectionIndex(currentDirectionIndex + _directionUtils.LeftTurnOffset);
        
        CurrentDirection = _directionUtils.OrderedDirections[newIndex];
    }

    public void TurnRight()
    {
        if (IsRoverLost) return;

        var currentDirectionIndex = Array.IndexOf(_directionUtils.OrderedDirections, CurrentDirection);
        var newIndex = _directionUtils.NormalizeDirectionIndex(currentDirectionIndex + _directionUtils.RightTurnOffset);
        
        CurrentDirection = _directionUtils.OrderedDirections[newIndex];
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
            if (_plateau.IsDangerZone(Position.X, Position.Y, CurrentDirection))
            {
                return;
            }
            
            _plateau.AddDangerZone(Position.X, Position.Y, CurrentDirection);
            IsRoverLost = true;
        }
    }
    
    public override string ToString()
    {
        return $"{Position.X} {Position.Y} {_directionMapper.DirectionToChar(CurrentDirection)}"
               + (IsRoverLost ? $" {RoverConstants.LostStatus}" : "");
    }
}