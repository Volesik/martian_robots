using MartianRobots.Common.Constants;

namespace MartianRobots.Domain.Entities;

public class Plateau
{
    public int MaxXPosition { get; }
    
    public int MaxYPosition { get; }

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
}