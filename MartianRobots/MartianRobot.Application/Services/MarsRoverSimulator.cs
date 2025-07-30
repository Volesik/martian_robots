using MartianRobot.Application.Interfaces;
using MartianRobot.Application.Models;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Interfaces;

namespace MartianRobot.Application.Services;

public class MarsRoverSimulator : IMarsRoverSimulator
{
    public IEnumerable<IRover> ProcessRoverInstructions(Plateau plateau, IEnumerable<RoverInstruction> roverInstructions)
    {
        var rovers = new List<Rover>();

        foreach (var instruction in roverInstructions)
        {
            var rover = new Rover(
                instruction.InitialPositionX,
                instruction.InitialPositionY,
                instruction.InitialDirection,
                plateau
            );
            
            ExecuteCommands(rover, instruction.Commands);
            rovers.Add(rover);
        }

        return rovers;
    }
    
    public IRover ExecuteCommands(IRover rover, string commandSequence)
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
                case 'M':
                    rover.MoveForward();
                    break;
                default:
                    throw new ArgumentException($"Invalid command character: '{instruction}'");
            }
        }

        return rover;
    }
}