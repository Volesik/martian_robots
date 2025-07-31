using FakeItEasy;
using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;
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
        _commandFactory = A.Fake<ICommandFactory>();
        _rover = A.Fake<IRover>();
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
}