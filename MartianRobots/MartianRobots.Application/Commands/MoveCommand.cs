using MartianRobots.Abstractions.Command;
using MartianRobots.Abstractions.Domains;

namespace MartianRobots.Application.Commands;

public class MoveCommand : ICommand
{
    public void Execute(IRover rover)
    {
        rover.MoveForward();
    }
}