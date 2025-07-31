using MartianRobots.Common.Enums;
using MartianRobots.Domain.Entities;
using MartianRobots.Dto.Mappers;
using MartianRobots.Dto.Requests;

namespace MartianRobots.Dto.Tests.Mappers;

[TestFixture]
public class RoverMapperTests
{
    [Test]
    public void ToModel_ShouldMapCommandToRoverCorrectly()
    {
        // Arrange
        var command = new RoverCommand
        {
            InitialPositionX = 2,
            InitialPositionY = 3,
            InitialDirection = 'E'
        };

        var plateau = new Plateau(5, 5);

        // Act
        var rover = RoverMapper.ToModel(command, plateau);
        
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
        Assert.Throws<ArgumentException>(() => RoverMapper.ToModel(command, plateau));
    }
    
    [Test]
    public void ToDto_ShouldMapRoverToMissionResult()
    {
        // Arrange
        var plateau = new Plateau(5, 5);
        var rover = new Rover(1, 2, Direction.North, plateau);
        rover.MoveForward();

        // Act
        var result = RoverMapper.ToDto(rover);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.FinalPositionX, Is.EqualTo(1));
            Assert.That(result.FinalPositionY, Is.EqualTo(3));
            Assert.That(result.FinalDirection, Is.EqualTo('N'));
            Assert.That(result.IsRoverLost, Is.False);
        });
    }

    [Test]
    public void ToDto_WhenRoverIsLost_ShouldSetIsRoverLostTrue()
    {
        // Arrange
        var plateau = new Plateau(2, 2);
        var rover = new Rover(2, 2, Direction.North, plateau);
        rover.MoveForward();

        // Act
        var result = RoverMapper.ToDto(rover);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsRoverLost, Is.True);
            Assert.That(result.FinalPositionX, Is.EqualTo(2));
            Assert.That(result.FinalPositionY, Is.EqualTo(2));
            Assert.That(result.FinalDirection, Is.EqualTo('N'));
        });
    }
}