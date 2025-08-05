using MartianRobots.Common.Enums;

namespace MartianRobots.Abstractions.Domains;

public interface IPlateau
{
    bool IsInsidePlateauArea(int xCoordinate, int yCoordinate);
    
    bool IsDangerZone(int xCoordinate, int yCoordinate, Direction direction);
    
    void AddDangerZone(int xCoordinate, int yCoordinate, Direction direction);
}