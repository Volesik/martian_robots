using MartianRobots.Abstractions.Domains;
using MartianRobots.Domain.Entities;
using MartianRobots.Dto.Requests;
using MartianRobots.Dto.Responses;

namespace MartianRobots.Application.Interfaces;

public interface IRoverMapper
{
    IRover ToModel(RoverCommand roverCommandDto, Plateau plateau);
    MissionResult ToDto(IRover rover);
}