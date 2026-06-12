using FruitSalesCalculator.Domain.Services;
using Xunit;

namespace FruitSalesCalculator.Tests.Services;

public class FruitCatalogTests
{
    [Fact]
    public void GetByName_ShouldReturnApple()
    {
        // Arrange

        var catalog =
            new InMemoryFruitCatalog();

        // Act

        var fruit =
            catalog.GetByName("Apple");

        // Assert

        Assert.AreEqual("Apple", fruit.Name);
    }

    [Fact]
    public void GetByName_ShouldBeCaseInsensitive()
    {
        // Arrange

        var catalog =
            new InMemoryFruitCatalog();

        // Act

        var fruit =
            catalog.GetByName("apple");

        // Assert

        Assert.AreEqual("Apple", fruit.Name);
    }

    [Fact]
    public void GetByName_ShouldThrow_WhenFruitDoesNotExist()
    {
        // Arrange

        var catalog =
            new InMemoryFruitCatalog();

        // Act & Assert

        Assert.Throws<KeyNotFoundException>(
            () => catalog.GetByName("Orange"));
    }
}