using FakeItEasy;
using MartianRobots.Abstractions.Domains;
using MartianRobots.Abstractions.Utils;
using MartianRobots.Application.Factories;
using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Utils;
using MartianRobots.Common.Enums;
using MartianRobots.Common.Models;

namespace MartianRobots.Domain.Tests.Entities;

[TestFixture]
public class RoverTests
{
    private IPlateau _plateau;
    private IDirectionMapper _directionMapper;
    private IDirectionUtils _directionUtils;

    [SetUp]
    public void SetUp()
    {
        _plateau = A.Fake<IPlateau>();
        _directionMapper = A.Fake<IDirectionMapper>();
        _directionUtils = new DirectionUtils();
    }

    [Test]
    public void Constructor_ValidPosition_ShouldInitializeRover()
    {
        // Arrange
        A.CallTo(() => _plateau.IsInsidePlateauArea(1, 1)).Returns(true);
        var roverFactory = new RoverFactory(_directionMapper, _directionUtils);
        
        // Act
        var rover = roverFactory.Create(1, 1, Direction.North, _plateau);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rover.Position.X, Is.EqualTo(1));
            Assert.That(rover.Position.Y, Is.EqualTo(1));
            Assert.That(rover.CurrentDirection, Is.EqualTo(Direction.North));
            Assert.That(rover.IsRoverLost, Is.False);
        });
    }
    
    [Test]
    public void Constructor_InvalidPosition_ShouldThrowError()
    {
        // Arrange
        A.CallTo(() => _plateau.IsInsidePlateauArea(10, 10)).Returns(false);
        var roverFactory = new RoverFactory(_directionMapper, _directionUtils);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var rover = roverFactory.Create(10, 10, Direction.East, _plateau);
        });
    }
    
    [Test]
    public void TurnLeft_WhenNotLost_ShouldChangeDirection()
    {
        // Arrange
        A.CallTo(() => _plateau.IsInsidePlateauArea(A<int>._, A<int>._)).Returns(true);
        var roverFactory = new RoverFactory(_directionMapper, _directionUtils);
        var rover = roverFactory.Create(1, 1, Direction.North, _plateau);
        
        // Act
        rover.TurnLeft();

        // Assert
        Assert.That(rover.CurrentDirection, Is.EqualTo(Direction.West));
    }
    
    [Test]
    public void MoveForward_ValidMove_ShouldUpdatePosition()
    {
        // Arrange
        A.CallTo(() => _plateau.IsInsidePlateauArea(1, 1)).Returns(true);
        A.CallTo(() => _plateau.IsInsidePlateauArea(1, 2)).Returns(true);
        var roverFactory = new RoverFactory(_directionMapper, _directionUtils);
        var rover = roverFactory.Create(1, 1, Direction.North, _plateau);

        // Act
        rover.MoveForward();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rover.Position, Is.EqualTo(new Coordinates(1, 2)));
            Assert.That(rover.IsRoverLost, Is.False);
        });
    }
    
    [Test]
    public void MoveForward_OffPlateau_NotInDangerZone_ShouldBecomeLost()
    {
        // Arrange
        A.CallTo(() => _plateau.IsInsidePlateauArea(1, 1)).Returns(true);
        A.CallTo(() => _plateau.IsInsidePlateauArea(1, 2)).Returns(false);
        A.CallTo(() => _plateau.IsDangerZone(1, 1, Direction.North)).Returns(false);
        var roverFactory = new RoverFactory(_directionMapper, _directionUtils);
        var rover = roverFactory.Create(1, 1, Direction.North, _plateau);

        // Act
        rover.MoveForward();

        // Assert
        A.CallTo(() => _plateau.AddDangerZone(1, 1, Direction.North)).MustHaveHappened();
        Assert.That(rover.IsRoverLost, Is.True);
    }
    
    [Test]
    public void MoveForward_IntoDangerZone_ShouldDoNothing()
    {
        // Arrange
        A.CallTo(() => _plateau.IsInsidePlateauArea(1, 1)).Returns(true);
        A.CallTo(() => _plateau.IsInsidePlateauArea(1, 2)).Returns(false);
        A.CallTo(() => _plateau.IsDangerZone(1, 1, Direction.North)).Returns(true);
        var roverFactory = new RoverFactory(_directionMapper, _directionUtils);
        var rover = roverFactory.Create(1, 1, Direction.North, _plateau);

        // Act
        rover.MoveForward();

        // Assert
        A.CallTo(() => _plateau.AddDangerZone(A<int>._, A<int>._, A<Direction>._)).MustNotHaveHappened();
        Assert.Multiple(() =>
        {
            Assert.That(rover.IsRoverLost, Is.False);
            Assert.That(rover.Position, Is.EqualTo(new Coordinates(1, 1)));
        });
    }
}