using FakeItEasy;
using MartianRobots.Abstractions.Domains;
using MartianRobots.Application.Commands;

namespace MartianRobots.Application.Tests.Command;

[TestFixture]
public class CommandTests
{
    [Test]
    public void MoveCommand_ShouldCallRover_MoveForward()
    {
        // Arrange
        var rover = A.Fake<IRover>();
        var command = new MoveCommand();

        // Act
        command.Execute(rover);

        // Assert
        A.CallTo(() => rover.MoveForward()).MustHaveHappened();
    }
    
    [Test]
    public void TurnLeftCommand_ShouldCallRover_TurnLeft()
    {
        // Arrange
        var rover = A.Fake<IRover>();
        var command = new TurnLeftCommand();

        // Act
        command.Execute(rover);

        // Assert
        A.CallTo(() => rover.TurnLeft()).MustHaveHappened();
    }
    
    [Test]
    public void TurnRightCommand_ShouldCallRover_TurnRight()
    {
        // Arrange
        var rover = A.Fake<IRover>();
        var command = new TurnRightCommand();

        // Act
        command.Execute(rover);

        // Assert
        A.CallTo(() => rover.TurnRight()).MustHaveHappened();
    }
}