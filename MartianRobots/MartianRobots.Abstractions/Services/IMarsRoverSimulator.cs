using MartianRobots.Abstractions.Domains;

namespace MartianRobots.Abstractions.Services;

public interface IMarsRoverSimulator
{
    void ExecuteCommands(IRover rover, string commandSequence);
}