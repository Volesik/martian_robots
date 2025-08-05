using MartianRobots.Abstractions.Command;

namespace MartianRobots.Abstractions.Factories;

public interface ICommandFactory
{
    ICommand Create(char instruction);
}