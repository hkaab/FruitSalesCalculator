using FruitSalesCalculator.Domain.Models;
using FruitSalesCalculator.Domain.Pricing;
using Xunit;

namespace FruitSalesCalculator.Tests.Pricing;

public class PricingStrategyTests
{
    [Fact]
    public void PerKgPricing_ShouldCalculateCorrectPrice()
    {
        // Arrange

        var strategy = new PerKgPricingStrategy();

        // Act

        var result =
            strategy.CalculatePrice(2m, 3m, new Domain.Models.OrderContext());

        // Assert

        Assert.AreEqual(6m, result);
    }

    [Fact]
    public void PerItemPricing_ShouldCalculateCorrectPrice()
    {
        // Arrange

        var strategy = new PerItemPricingStrategy();

        // Act

        var result =
            strategy.CalculatePrice(0.30m, 10m, new Domain.Models.OrderContext());

        // Assert

        Assert.AreEqual(3m, result);
    }

    [Fact]
    public void DiscountedPricing_ShouldNotApplyDiscount_WhenThresholdNotReached()
    {
        // Arrange

        var strategy =
            new DiscountedKgPricingStrategy(
                thresholdKg: 2,
                discountPercentage: 10);

        // Act

        var result =
            strategy.CalculatePrice(
                5m,
                2m, new Domain.Models.OrderContext());

        // Assert

        Assert.AreEqual(10m, result);
    }

    [Fact]
    public void DiscountedPricing_ShouldApplyDiscount_WhenThresholdExceeded()
    {
        // Arrange

        var strategy =
            new DiscountedKgPricingStrategy(
                thresholdKg: 2,
                discountPercentage: 10);

        // Act

        var result =
            strategy.CalculatePrice(
                5m,
                3m, new OrderContext());

        // Assert

        Assert.AreEqual(13.5m, result);
    }

    [Theory]
    [InlineData(12, 9, 108)] // No discount
    [InlineData(12, 12, 129.6)] // Discount applied
    public void BulkDiscountDecorator_ShouldApplyBulkDiscount_WhenThresholdExceeded(
        decimal basePrice,
        decimal quantity,
        decimal expectedPrice)
    {
        // Arrange
        var strategy =
            new BulkDiscountDecorator(
                new PerKgPricingStrategy());
        // Act
        var result =
            strategy.CalculatePrice(
                basePrice,
                quantity,new OrderContext());
        // Assert
        Assert.AreEqual(expectedPrice, result);
    }
    [Fact]
    public void BulkDiscountDecorator_ShouldThrowException_WhenInnerStrategyIsNull()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            var strategy =
                new BulkDiscountDecorator(null!);
        });
    }
    [Theory]
    [InlineData(12, 12, 136.8)] // Discount applied
    public void LoyaltyDiscountDecorator_ShouldApplyLoyaltyDiscount_WhenCustomerIsLoyal(
        decimal basePrice,
        decimal quantity,
        decimal expectedPrice)
    {
        // Arrange
        var strategy =
            new LoyaltyDiscountDecorator(
                new PerKgPricingStrategy());
        // Act
        var result =
            strategy.CalculatePrice(
                basePrice,
                quantity, new Domain.Models.OrderContext { IsLoyalCustomer = true }
                );
        // Assert
        Assert.AreEqual(expectedPrice, result);
    }
}