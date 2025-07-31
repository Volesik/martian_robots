using MartianRobots.Domain.Interfaces;

namespace MartianRobots.Application.Interfaces;

public interface ICommand
{
    void Execute(IRover rover);
}