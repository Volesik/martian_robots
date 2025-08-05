using MartianRobots.Abstractions.Factories;
using MartianRobots.Application.Commands;
using MartianRobots.Application.Interfaces;

namespace MartianRobots.Application.Tests.Command;

[TestFixture]
public class CommandFactoryTests
{
    private ICommandFactory _factory;

    [SetUp]
    public void SetUp()
    {
        _factory = new CommandFactory();
    }
    
    [TestCase('F', typeof(MoveCommand))]
    [TestCase('f', typeof(MoveCommand))]
    [TestCase('L', typeof(TurnLeftCommand))]
    [TestCase('l', typeof(TurnLeftCommand))]
    [TestCase('R', typeof(TurnRightCommand))]
    [TestCase('r', typeof(TurnRightCommand))]
    public void Create_ValidChar_ReturnsExpectedCommandType(char input, Type expectedType)
    {
        // Act
        var command = _factory.Create(input);
        
        // Assert
        Assert.That(command, Is.InstanceOf(expectedType));
    }
    
    [TestCase('X')]
    [TestCase(' ')]
    [TestCase('1')]
    public void Create_InvalidChar_ThrowsArgumentException(char input)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _factory.Create(input));
    }
}