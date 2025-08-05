using MartianRobots.Abstractions.Factories;
using MartianRobots.Abstractions.Utils;
using MartianRobots.Application.Factories;
using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Mappers;
using MartianRobots.Application.Services;
using MartianRobots.Application.Utils;
using MartianRobots.Common.Constants;
using MartianRobots.Common.Enums;
using MartianRobots.Domain.Entities;
using MartianRobots.Dto.Requests;

namespace MartianRobots.Application.Tests.Mappers;

[TestFixture]
public class RoverMapperTests
{
    private IRoverMapper _roverMapper;
    private IDirectionUtils _directionUtils;
    private IDirectionMapper _directionMapper;
    private IRoverFactory _roverFactory;

    [SetUp]
    public void SetUp()
    {
        _directionMapper = new DirectionMapper();
        _directionUtils = new DirectionUtils();
        _roverFactory = new RoverFactory(_directionMapper, _directionUtils);
        _roverMapper = new RoverMapper(_directionMapper, _roverFactory);
    }
    
    [Test]
    public void ToModel_ShouldMapCommandToRoverCorrectly()
    {
        // Arrange
        var command = new RoverCommand
        {
            InitialPositionX = 2,
            InitialPositionY = 3,
            InitialDirection = DirectionConstants.EastDirection
        };

        var plateau = new Plateau(5, 5);

        // Act
        var rover = _roverMapper.ToModel(command, plateau);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rover.Position.X, Is.EqualTo(2));
            Assert.That(rover.Position.Y, Is.EqualTo(3));
            Assert.That(rover.CurrentDirection, Is.EqualTo(Direction.East));
            Assert.That(rover.IsRoverLost, Is.False);
        });
    }
    
    [Test]
    public void ToModel_InvalidDirection_ShouldThrowException()
    {
        // Arrange
        var command = new RoverCommand
        {
            InitialPositionX = 0,
            InitialPositionY = 0,
            InitialDirection = 'X'
        };
        var plateau = new Plateau(5, 5);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _roverMapper.ToModel(command, plateau));
    }
    
    [Test]
    public void ToDto_ShouldMapRoverToMissionResult()
    {
        // Arrange
        var plateau = new Plateau(5, 5);
        var roverFactory = new RoverFactory(_directionMapper, _directionUtils);
        var rover = roverFactory.Create(1, 2, Direction.North, plateau);
        rover.MoveForward();

        // Act
        var result = _roverMapper.ToDto(rover);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.FinalPositionX, Is.EqualTo(1));
            Assert.That(result.FinalPositionY, Is.EqualTo(3));
            Assert.That(result.FinalDirection, Is.EqualTo(DirectionConstants.NorthDirection));
            Assert.That(result.IsRoverLost, Is.False);
        });
    }

    [Test]
    public void ToDto_WhenRoverIsLost_ShouldSetIsRoverLostTrue()
    {
        // Arrange
        var plateau = new Plateau(2, 2);
        var roverFactory = new RoverFactory(_directionMapper, _directionUtils);
        var rover = roverFactory.Create(2, 2, Direction.North, plateau);
        rover.MoveForward();

        // Act
        var result = _roverMapper.ToDto(rover);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsRoverLost, Is.True);
            Assert.That(result.FinalPositionX, Is.EqualTo(2));
            Assert.That(result.FinalPositionY, Is.EqualTo(2));
            Assert.That(result.FinalDirection, Is.EqualTo(DirectionConstants.NorthDirection));
        });
    }
}