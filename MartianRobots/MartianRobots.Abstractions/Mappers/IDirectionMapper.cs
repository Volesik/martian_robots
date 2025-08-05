using MartianRobots.Common.Enums;

namespace MartianRobots.Application.Interfaces;

public interface IDirectionMapper
{
    Direction CharToDirection(char direction);
    char DirectionToChar(Direction direction);
}