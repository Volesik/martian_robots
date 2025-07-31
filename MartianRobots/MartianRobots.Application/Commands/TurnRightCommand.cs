using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Interfaces;

namespace MartianRobots.Application.Commands;

public class TurnRightCommand : ICommand
{
    public void Execute(IRover rover)
    {
        rover.TurnRight();
    }
}