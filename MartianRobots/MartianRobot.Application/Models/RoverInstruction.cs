using MartianRobots.Common.Enums;

namespace MartianRobot.Application.Models;

public readonly record struct RoverInstruction(
    int InitialPositionX,
    int InitialPositionY,
    Direction InitialDirection,
    string Commands
);