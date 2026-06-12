using FruitSalesCalculator.Domain.Models;
using FruitSalesCalculator.Domain.Pricing;
using Xunit;

namespace FruitSalesCalculator.Tests.Models;

public class OrderItemTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenQuantityIsZero()
    {
        var fruit =
            new Fruit(
                "Apple",
                2m,
                new PerKgPricingStrategy());

        Assert.Throws<ArgumentException>(
            () => new OrderItem(
                fruit,
                0));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenQuantityIsNegative()
    {
        var fruit =
            new Fruit(
                "Apple",
                2m,
                new PerKgPricingStrategy());

        Assert.Throws<ArgumentException>(
            () => new OrderItem(
                fruit,
                -1));
    }
}