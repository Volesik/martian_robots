using MartianRobots.Abstractions.Domains;

namespace MartianRobots.Abstractions.Command;

public interface ICommand
{
    void Execute(IRover rover);
}