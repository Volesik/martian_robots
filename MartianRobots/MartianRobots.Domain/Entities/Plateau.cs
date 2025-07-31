using MartianRobots.Common.Constants;
using MartianRobots.Common.Enums;
using MartianRobots.Domain.Interfaces;

namespace MartianRobots.Domain.Entities;

public class Plateau : IPlateau
{
    public int MaxXPosition { get; }
    
    public int MaxYPosition { get; }
    
    private readonly HashSet<(int xCoordinate, int yCoordinate, Direction direction)> _dangerZones = [];

    public Plateau(int maxXPosition, int maxYPosition)
    {
        if (maxXPosition <= PlateauConstants.DefaultPosition || maxYPosition <= PlateauConstants.DefaultPosition)
        {
            throw new ArgumentException($"Plateau dimensions must be greater than {PlateauConstants.DefaultPosition}.");
        }
        
        MaxXPosition = maxXPosition;
        MaxYPosition = maxYPosition;
    }

    public bool IsInsidePlateauArea(int x, int y)
    {
        var isInsidePlateau = x >= PlateauConstants.DefaultPosition && x <= MaxXPosition &&
                              y >= PlateauConstants.DefaultPosition && y <= MaxYPosition;
        
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