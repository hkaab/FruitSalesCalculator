using FruitSalesCalculator.Domain.Models;
using FruitSalesCalculator.Domain.Pricing;
using FruitSalesCalculator.Domain.Services;
using Xunit;

namespace FruitSalesCalculator.Tests.Services;

public class OrderCalculatorTests
{
    [Fact]
    public void CalculateTotal_ShouldReturnZero_ForEmptyOrder()
    {
        // Arrange

        var order = new Order();

        var calculator =
            new OrderCalculator();

        // Act

        var total =
            calculator.CalculateTotal(order);

        // Assert

        Assert.AreEqual(0m, total);
    }

    [Fact]
    public void CalculateTotal_ShouldCalculateCorrectTotal_ForMixedOrder()
    {
        // Arrange

        var order = new Order();

        order.AddItem(
            new Fruit(
                "Apple",
                2m,
                new PerKgPricingStrategy()),
            3);

        order.AddItem(
            new Fruit(
                "Banana",
                0.3m,
                new PerItemPricingStrategy()),
            10);

        order.AddItem(
            new Fruit(
                "Cherry",
                5m,
                new DiscountedKgPricingStrategy(
                    2m,
                    10m)),
            3);

        var calculator =
            new OrderCalculator();

        // Act

        var total =
            calculator.CalculateTotal(order);

        // Assert

        Assert.AreEqual(22.5m, total);
    }
}