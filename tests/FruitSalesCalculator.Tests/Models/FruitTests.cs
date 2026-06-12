using FruitSalesCalculator.Domain.Models;
using FruitSalesCalculator.Domain.Pricing;
using Xunit;

namespace FruitSalesCalculator.Tests.Models;

public class FruitTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenNameIsEmpty()
    {
        // Arrange, Act, Assert

        Assert.Throws<ArgumentException>(
            () => new Fruit(
                "",
                2m,
                new PerKgPricingStrategy()));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenPriceIsInvalid()
    {
        // Arrange, Act, Assert

        Assert.Throws<ArgumentException>(
            () => new Fruit(
                "Apple",
                0m,
                new PerKgPricingStrategy()));
    }
}