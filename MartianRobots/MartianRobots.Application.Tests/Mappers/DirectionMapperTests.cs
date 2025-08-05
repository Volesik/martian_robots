using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Services;
using MartianRobots.Common.Constants;
using MartianRobots.Common.Enums;

namespace MartianRobots.Application.Tests.Mappers;

[TestFixture]
public class DirectionMapperTests
{
    private IDirectionMapper _directionMapper;
    
    [SetUp]
    public void SetUp()
    {
        _directionMapper = new DirectionMapper();
    }
    
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
        var result = _directionMapper.CharToDirection(input);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [TestCase('X')]
    [TestCase('Z')]
    [TestCase('1')]
    [TestCase('-')]
    public void CharToDirection_InvalidInputs_ShouldThrowArgumentException(char input)
    {
        var exception = Assert.Throws<ArgumentException>(() => _directionMapper.CharToDirection(input));
        Assert.That(exception.Message, Does.Contain("Invalid direction"));
    }
    
    [TestCase(Direction.North, DirectionConstants.NorthDirection)]
    [TestCase(Direction.East, DirectionConstants.EastDirection)]
    [TestCase(Direction.South, DirectionConstants.SouthDirection)]
    [TestCase(Direction.West, DirectionConstants.WestDirection)]
    public void DirectionToChar_ValidDirection_ReturnsExpectedChar(Direction input, char expected)
    {
        var result = _directionMapper.DirectionToChar(input);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void DirectionToChar_InvalidEnum_ReturnsQuestionMark()
    {
        var invalidDirection = (Direction)(-1);
        var result = _directionMapper.DirectionToChar(invalidDirection);
        
        Assert.That(result, Is.EqualTo(DirectionConstants.UnknownDirection));
    }
}