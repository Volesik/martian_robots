using MartianRobots.Abstractions.Domains;
using MartianRobots.Abstractions.Factories;
using MartianRobots.Abstractions.Services;

namespace MartianRobots.Application.Services;

public class MarsRoverSimulator : IMarsRoverSimulator
{
    private readonly ICommandFactory _commandFactory;

    public MarsRoverSimulator(ICommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
    }
    
    public void ExecuteCommands(IRover rover, string commandSequence)
    {
        foreach (var instruction in commandSequence.ToUpperInvariant())
        {
            var command = _commandFactory.Create(instruction);
            command.Execute(rover);
        }
    }
}