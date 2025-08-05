using MartianRobots.Abstractions.Domains;
using MartianRobots.Common.Enums;

namespace MartianRobots.Abstractions.Factories;

public interface IRoverFactory
{
    IRover Create(int x, int y, Direction direction, IPlateau plateau);
}