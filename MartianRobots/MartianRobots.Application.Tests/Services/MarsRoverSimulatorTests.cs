using FakeItEasy;
using MartianRobots.Application.Commands;
using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;
using MartianRobots.Common.Enums;
using MartianRobots.Domain.Entities;
using MartianRobots.Domain.Interfaces;

namespace MartianRobots.Application.Tests.Services;

[TestFixture]
public class MarsRoverSimulatorTests
{
    private ICommandFactory _commandFactory;
    private IMarsRoverSimulator _marsRoverSimulator;
    private IRover _rover;

    [SetUp]
    public void SetUp()
    {
        _rover = A.Fake<IRover>();
        _commandFactory = new CommandFactory();
        _marsRoverSimulator = new MarsRoverSimulator(_commandFactory);
    }
    
    [Test]
    public void ExecuteCommands_ValidSequence_ShouldCallFactoryAndExecuteEachCommand()
    {
        // Arrange
        var commands = "FLR";
        var fakeMove = A.Fake<ICommand>();
        var fakeLeft = A.Fake<ICommand>();
        var fakeRight = A.Fake<ICommand>();

        A.CallTo(() => _commandFactory.Create('F')).Returns(fakeMove);
        A.CallTo(() => _commandFactory.Create('L')).Returns(fakeLeft);
        A.CallTo(() => _commandFactory.Create('R')).Returns(fakeRight);

        // Act
        _marsRoverSimulator.ExecuteCommands(_rover, commands);

        // Assert
        A.CallTo(() => _commandFactory.Create('F')).MustHaveHappenedOnceExactly();
        A.CallTo(() => _commandFactory.Create('L')).MustHaveHappenedOnceExactly();
        A.CallTo(() => _commandFactory.Create('R')).MustHaveHappenedOnceExactly();

        A.CallTo(() => fakeMove.Execute(_rover)).MustHaveHappenedOnceExactly();
        A.CallTo(() => fakeLeft.Execute(_rover)).MustHaveHappenedOnceExactly();
        A.CallTo(() => fakeRight.Execute(_rover)).MustHaveHappenedOnceExactly();
    }
    
    [Test]
    public void ExecuteCommands_ShouldHandleLowercaseInput()
    {
        // Arrange
        var fakeCommand = A.Fake<ICommand>();
        A.CallTo(() => _commandFactory.Create('F')).Returns(fakeCommand);

        // Act
        _marsRoverSimulator.ExecuteCommands(_rover, "f");

        // Assert
        A.CallTo(() => _commandFactory.Create('F')).MustHaveHappenedOnceExactly(); // Capital
        A.CallTo(() => fakeCommand.Execute(_rover)).MustHaveHappenedOnceExactly();
    }
    
    [Test]
    public void ExecuteCommands_EmptyString_ShouldDoNothing()
    {
        // Act
        _marsRoverSimulator.ExecuteCommands(_rover, string.Empty);

        // Assert
        A.CallTo(() => _commandFactory.Create(A<char>._)).MustNotHaveHappened();
    }

    [Test]
    public void ExecuteCommands_ShouldSimulateSampleInputCorrectly()
    {
        // Arrange
        var plateau = new Plateau(5, 3);
        
        var rover1 = new Rover(1, 1, Direction.East, plateau);
        var rover2 = new Rover(3, 2, Direction.North, plateau);
        var rover3 = new Rover(0, 3, Direction.West, plateau);
        
        // Act
        _marsRoverSimulator.ExecuteCommands(rover1, "RFRFRFRF");
        _marsRoverSimulator.ExecuteCommands(rover2, "FRRFLLFFRRFLL");
        _marsRoverSimulator.ExecuteCommands(rover3, "LLFFFLFLFL");
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rover1.ToString(), Is.EqualTo("1 1 E"));
            Assert.That(rover2.ToString(), Is.EqualTo("3 3 N LOST"));
            Assert.That(rover3.ToString(), Is.EqualTo("2 3 S"));
        });
    }
}