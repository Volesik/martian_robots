using MartianRobots.Common.Validators;
using NUnit.Framework;

namespace MartianRobots.Common.Tests.Validators;

[TestFixture]
public class InstructionValidatorTests
{
    [Test]
    public void ValidateCoordinates_WithinLimit_ShouldNotThrowException()
    {
        Assert.DoesNotThrow(() => InstructionValidator.ValidateCoordinates(25, 30));
    }

    [Test]
    public void ValidateCoordinates_AboveLimit_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => InstructionValidator.ValidateCoordinates(51, 20));
        Assert.Throws<ArgumentException>(() => InstructionValidator.ValidateCoordinates(10, 55));
    }

    [Test]
    public void ValidateCoordinates_Negative_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => InstructionValidator.ValidateCoordinates(-1, 0));
        Assert.Throws<ArgumentException>(() => InstructionValidator.ValidateCoordinates(0, -5));
    }

    [Test]
    public void ValidateInstructionLength_Valid_ShouldNotThrowException()
    {
        var validCommands = new string('L', 99);
        Assert.DoesNotThrow(() => InstructionValidator.ValidateInstructionLength(validCommands));
    }

    [Test]
    public void ValidateInstructionLength_TooLong_ShouldThrowException()
    {
        var tooLong = new string('F', 100);
        Assert.Throws<ArgumentException>(() => InstructionValidator.ValidateInstructionLength(tooLong));
    }

    [Test]
    public void ValidateInstructionLength_EmptyOrNull_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => InstructionValidator.ValidateInstructionLength(""));
        Assert.Throws<ArgumentException>(() => InstructionValidator.ValidateInstructionLength("  "));
        Assert.Throws<ArgumentException>(() => InstructionValidator.ValidateInstructionLength(null));
    }
}