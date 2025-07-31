using MartianRobots.Common.Enums;
using MartianRobots.Common.Extensions;
using NUnit.Framework;

namespace MartianRobots.Common.Tests.Extensions;

[TestFixture]
public class DirectionExtensionsTests
{
    [TestCase(Direction.North, Direction.West)]
    [TestCase(Direction.West, Direction.South)]
    [TestCase(Direction.South, Direction.East)]
    [TestCase(Direction.East, Direction.North)]
    public void TurnLeft_ShouldReturnExpectedResult(Direction input, Direction expected)
    {
        var result = input.TurnLeft();
        
        Assert.That(expected, Is.EqualTo(result));
    }
    
    [TestCase(Direction.North, Direction.East)]
    [TestCase(Direction.East, Direction.South)]
    [TestCase(Direction.South, Direction.West)]
    [TestCase(Direction.West, Direction.North)]
    public void TurnRight_ShouldReturnExpectedResult(Direction input, Direction expected)
    {
        var result = input.TurnRight();
        
        Assert.That(expected, Is.EqualTo(result));
    }
}