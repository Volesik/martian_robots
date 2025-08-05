using MartianRobots.Abstractions.Domains;
using MartianRobots.Abstractions.Factories;
using MartianRobots.Abstractions.Utils;
using MartianRobots.Application.Interfaces;
using MartianRobots.Common.Enums;
using MartianRobots.Domain.Entities;

namespace MartianRobots.Application.Factories;

public class RoverFactory : IRoverFactory
{
    private readonly IDirectionMapper _directionMapper;
    private readonly IDirectionUtils _directionUtils;

    public RoverFactory(
        IDirectionMapper directionMapper,
        IDirectionUtils directionUtils)
    {
        _directionMapper = directionMapper;
        _directionUtils = directionUtils;
    }
    
    public IRover Create(int x, int y, Direction direction, IPlateau plateau)
    {
        return new Rover(x, y, direction, plateau, _directionMapper, _directionUtils);
    }
}