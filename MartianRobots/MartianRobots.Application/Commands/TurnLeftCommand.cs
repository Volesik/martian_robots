using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Interfaces;

namespace MartianRobots.Application.Commands;

public class TurnLeftCommand : ICommand
{
    public void Execute(IRover rover)
    {
        rover.TurnLeft();
    }
}