using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Interfaces;

namespace MartianRobots.Application.Commands;

public class MoveCommand : ICommand
{
    public void Execute(IRover rover)
    {
        rover.MoveForward();
    }
}