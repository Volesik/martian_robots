using MartianRobot.Application.Models;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Interfaces;

namespace MartianRobot.Application.Interfaces;

public interface IMarsRoverSimulator
{
    IEnumerable<IRover> ProcessRoverInstructions(Plateau plateau, IEnumerable<RoverInstruction> roverInstructions);
    
    IRover ExecuteCommands(IRover rover, string commandSequence);
}