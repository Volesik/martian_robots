using MartianRobots.Abstractions.Domains;
using MartianRobots.Common.Constants;
using MartianRobots.Common.Enums;

namespace MartianRobots.Domain.Entities;

public class Plateau : IPlateau
{
    public int MaxXPosition { get; }
    
    public int MaxYPosition { get; }
    
    private readonly HashSet<(int xCoordinate, int yCoordinate, Direction direction)> _dangerZones = [];

    public Plateau(int maxXPosition, int maxYPosition)
    {
        if (maxXPosition <= CoordinateConstants.DefaultPosition || maxYPosition <= CoordinateConstants.DefaultPosition)
        {
            throw new ArgumentException($"Plateau dimensions must be greater than {CoordinateConstants.DefaultPosition}.");
        }
        
        MaxXPosition = maxXPosition;
        MaxYPosition = maxYPosition;
    }

    public bool IsInsidePlateauArea(int xCoordinate, int yCoordinate)
    {
        var isInsidePlateau = xCoordinate >= CoordinateConstants.DefaultPosition && xCoordinate <= MaxXPosition &&
                              yCoordinate >= CoordinateConstants.DefaultPosition && yCoordinate <= MaxYPosition;
        
        return isInsidePlateau;
    }

    public bool IsDangerZone(int xCoordinate, int yCoordinate, Direction direction)
    {
        var isDangerZone = _dangerZones.Contains((xCoordinate, yCoordinate, direction));
        
        return isDangerZone;
    }

    public void AddDangerZone(int xCoordinate, int yCoordinate, Direction direction)
    {
        _dangerZones.Add((xCoordinate, yCoordinate, direction));
    }
}