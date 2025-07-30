using MartianRobot.Application.Interfaces;
using MartianRobots.Domain.Interfaces;

namespace MartianRobot.Application.Services;

public class MarsRoverSimulator : IMarsRoverSimulator
{
    public void ExecuteCommands(IRover rover, string commandSequence)
    {
        foreach (var instruction in commandSequence.ToUpperInvariant())
        {
            switch (instruction)
            {
                case 'L':
                    rover.TurnLeft();
                    break;
                case 'R':
                    rover.TurnRight();
                    break;
                case 'F':
                    rover.MoveForward();
                    break;
                default:
                    throw new ArgumentException($"Invalid command character: '{instruction}'");
            }
        }
    }
}