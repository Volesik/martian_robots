using MartianRobots.Common.Enums;

namespace MartianRobots.Domain.Interfaces;

public interface IPlateau
{
    bool IsInsidePlateauArea(int x, int y);
    
    bool IsDangerZone(int xCoordinate, int yCoordinate, Direction direction);
    
    void AddDangerZone(int xCoordinate, int yCoordinate, Direction direction);
}