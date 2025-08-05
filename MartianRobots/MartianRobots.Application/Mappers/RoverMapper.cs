using MartianRobots.Abstractions.Domains;
using MartianRobots.Abstractions.Factories;
using MartianRobots.Application.Interfaces;
using MartianRobots.Domain.Entities;
using MartianRobots.Dto.Requests;
using MartianRobots.Dto.Responses;

namespace MartianRobots.Application.Mappers;

public class RoverMapper : IRoverMapper
{
    private readonly IDirectionMapper _directionMapper;
    private readonly IRoverFactory _roverFactory;

    public RoverMapper(
        IDirectionMapper directionMapper,
        IRoverFactory roverFactory)
    {
        _directionMapper = directionMapper;
        _roverFactory = roverFactory;
    }
    
    public IRover ToModel(RoverCommand roverCommandDto, Plateau plateau)
    {
        var direction = _directionMapper.CharToDirection(roverCommandDto.InitialDirection);
        var rover = _roverFactory.Create(roverCommandDto.InitialPositionX, roverCommandDto.InitialPositionY, direction, plateau);
        
        return rover;
    }

    public MissionResult ToDto(IRover rover)
    {
        var missionResult = new MissionResult
        {
            FinalPositionX = rover.Position.X,
            FinalPositionY = rover.Position.Y,
            FinalDirection = _directionMapper.DirectionToChar(rover.CurrentDirection),
            IsRoverLost = rover.IsRoverLost
        };
        
        return missionResult;
    }
}