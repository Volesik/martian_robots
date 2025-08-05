using MartianRobots.Common.Mappers;
using MartianRobots.Domain.Entities;
using MartianRobots.Dto.Requests;
using MartianRobots.Dto.Responses;

namespace MartianRobots.Dto.Mappers;

public class RoverMapper
{
    public static Rover ToModel(RoverCommand roverCommandDto, Plateau plateau)
    {
        var direction = DirectionMapper.CharToDirection(roverCommandDto.InitialDirection);
        var rover = new Rover(roverCommandDto.InitialPositionX, roverCommandDto.InitialPositionY, direction, plateau);
        
        return rover;
    }

    public static MissionResult ToDto(Rover rover)
    {
        var missionResult = new MissionResult
        {
            FinalPositionX = rover.Position.X,
            FinalPositionY = rover.Position.Y,
            FinalDirection = DirectionMapper.DirectionToChar(rover.CurrentDirection),
            IsRoverLost = rover.IsRoverLost
        };
        
        return missionResult;
    }
}