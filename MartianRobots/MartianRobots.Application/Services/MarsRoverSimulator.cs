using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Interfaces;

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