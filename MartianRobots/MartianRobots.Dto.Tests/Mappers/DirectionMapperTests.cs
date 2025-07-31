using MartianRobots.Common.Enums;
using MartianRobots.Dto.Mappers;

namespace MartianRobots.Dto.Tests.Mappers;

[TestFixture]
public class DirectionMapperTests
{
    [TestCase('N', Direction.North)]
    [TestCase('E', Direction.East)]
    [TestCase('S', Direction.South)]
    [TestCase('W', Direction.West)]
    [TestCase('n', Direction.North)]
    [TestCase('e', Direction.East)]
    [TestCase('s', Direction.South)]
    [TestCase('w', Direction.West)]
    public void CharToDirection_ValidInputs_ShouldReturnExpectedDirection(char input, Direction expected)
    {
        var result = DirectionMapper.CharToDirection(input);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [TestCase('X')]
    [TestCase('Z')]
    [TestCase('1')]
    [TestCase('-')]
    public void CharToDirection_InvalidInputs_ShouldThrowArgumentException(char input)
    {
        var exception = Assert.Throws<ArgumentException>(() => DirectionMapper.CharToDirection(input));
        Assert.That(exception.Message, Does.Contain("Invalid direction"));
    }
    
    [TestCase(Direction.North, 'N')]
    [TestCase(Direction.East, 'E')]
    [TestCase(Direction.South, 'S')]
    [TestCase(Direction.West, 'W')]
    public void DirectionToChar_ValidDirection_ReturnsExpectedChar(Direction input, char expected)
    {
        var result = DirectionMapper.DirectionToChar(input);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void DirectionToChar_InvalidEnum_ReturnsQuestionMark()
    {
        var invalidDirection = (Direction)(-1);
        var result = DirectionMapper.DirectionToChar(invalidDirection);
        
        Assert.That(result, Is.EqualTo('?'));
    }
}