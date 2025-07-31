using MartianRobots.Application.Interfaces;

namespace MartianRobots.Application.Commands;

public class CommandFactory : ICommandFactory
{
    public ICommand Create(char instruction)
    {
        return char.ToUpperInvariant(instruction) switch
        {
            'F' => new MoveCommand(),
            'L' => new TurnLeftCommand(),
            'R' => new TurnRightCommand(),
            _ => throw new ArgumentException($"Unknown instruction: {instruction}")
        };
    }
}