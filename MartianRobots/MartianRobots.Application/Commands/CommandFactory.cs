using MartianRobots.Application.Interfaces;
using MartianRobots.Common.Constants;

namespace MartianRobots.Application.Commands;

public class CommandFactory : ICommandFactory
{
    public ICommand Create(char instruction)
    {
        return char.ToUpperInvariant(instruction) switch
        {
            RoverConstants.MoveForwardInstruction => new MoveCommand(),
            RoverConstants.TurnLeftInstruction => new TurnLeftCommand(),
            RoverConstants.TurnRightInstruction => new TurnRightCommand(),
            _ => throw new ArgumentException($"Unknown instruction: {instruction}")
        };
    }
}