using MartianRobots.Abstractions.Utils;
using MartianRobots.Application.Utils;

namespace MartianRobots.Application.Tests.Utils;

[TestFixture]
public class InstructionValidatorTests
{
    private IInstructionValidator _instructionValidator;
    
    [SetUp]
    public void Setup()
    {
        _instructionValidator = new InstructionValidator();
    }
    
    [Test]
    public void ValidateCoordinates_WithinLimit_ShouldNotThrowException()
    {
        Assert.DoesNotThrow(() => _instructionValidator.ValidateCoordinates(25, 30));
    }

    [Test]
    public void ValidateCoordinates_AboveLimit_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => _instructionValidator.ValidateCoordinates(51, 20));
        Assert.Throws<ArgumentException>(() => _instructionValidator.ValidateCoordinates(10, 55));
    }

    [Test]
    public void ValidateCoordinates_Negative_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => _instructionValidator.ValidateCoordinates(-1, 0));
        Assert.Throws<ArgumentException>(() => _instructionValidator.ValidateCoordinates(0, -5));
    }

    [Test]
    public void ValidateInstructionLength_Valid_ShouldNotThrowException()
    {
        var validCommands = new string('L', 99);
        Assert.DoesNotThrow(() => _instructionValidator.ValidateInstructionLength(validCommands));
    }

    [Test]
    public void ValidateInstructionLength_TooLong_ShouldThrowException()
    {
        var tooLong = new string('F', 100);
        Assert.Throws<ArgumentException>(() => _instructionValidator.ValidateInstructionLength(tooLong));
    }

    [Test]
    public void ValidateInstructionLength_EmptyOrNull_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => _instructionValidator.ValidateInstructionLength(""));
        Assert.Throws<ArgumentException>(() => _instructionValidator.ValidateInstructionLength("  "));
        Assert.Throws<ArgumentException>(() => _instructionValidator.ValidateInstructionLength(null));
    }
}