using MartianRobot.Application.Models;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Interfaces;

namespace MartianRobot.Application.Interfaces;

public interface IMarsRoverSimulator
{
    void ExecuteCommands(IRover rover, string commandSequence);
}