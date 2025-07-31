using MartianRobots.Common.Constants;
using MartianRobots.Common.Enums;
using MartianRobots.Domain.Entities;

namespace MartianRobots.Domain.Tests.Entities;

[TestFixture]
public class PlateauTests
{
    private Plateau _plateau;

    [SetUp]
    public void SetUp()
    {
        _plateau = new Plateau(5, 3);
    }
    
    [Test]
    public void Constructor_WithValidDimensions_ShouldInitializeCorrectly()
    {
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(_plateau.MaxXPosition, Is.EqualTo(5));
            Assert.That(_plateau.MaxYPosition, Is.EqualTo(3));
        });
    }
    
    [TestCase(0, 3)]
    [TestCase(5, 0)]
    [TestCase(0, 0)]
    [TestCase(-1, 5)]
    [TestCase(5, -1)]
    public void Constructor_WithInvalidDimensions_ShouldThrowError(int x, int y)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
        {
            var plateau = new Plateau(x, y);
        });
        StringAssert.Contains($"Plateau dimensions must be greater than {CoordinateConstants.DefaultPosition}", ex.Message);
    }
    
    [TestCase(0, 0, true)]
    [TestCase(5, 3, true)]
    [TestCase(2, 2, true)]
    [TestCase(6, 3, false)]
    [TestCase(5, 4, false)]
    [TestCase(-1, 0, false)]
    public void IsInsidePlateauArea_ShouldReturnExpectedResult(int x, int y, bool expected)
    {
        // Act
        var result = _plateau.IsInsidePlateauArea(x, y);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void AddDangerZone_ShouldRegisterAsDangerous()
    {
        // Arrange
        var x = 2;
        var y = 2;
        var direction = Direction.West;

        // Act
        _plateau.AddDangerZone(x, y, direction);

        // Assert
        Assert.That(_plateau.IsDangerZone(x, y, direction), Is.True);
    }
    
    [Test]
    public void IsDangerZone_ShouldReturnFalse_WhenNotMarked()
    {
        // Act
        var result = _plateau.IsDangerZone(1, 1, Direction.East);

        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void AddDangerZone_ShouldBeIdempotent()
    {
        // Arrange
        var x = 1;
        var y = 1;
        var direction = Direction.South;

        // Act
        _plateau.AddDangerZone(x, y, direction);
        _plateau.AddDangerZone(x, y, direction);

        // Assert
        Assert.That(_plateau.IsDangerZone(x, y, direction), Is.True);
    }
}