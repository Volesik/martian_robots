using MartianRobots.Domain.Interfaces;

namespace MartianRobots.Application.Interfaces;

public interface IMarsRoverSimulator
{
    void ExecuteCommands(IRover rover, string commandSequence);
}