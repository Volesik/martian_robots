namespace MartianRobots.Application.Interfaces;

public interface ICommandFactory
{
    ICommand Create(char instruction);
}