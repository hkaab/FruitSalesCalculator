namespace FruitSalesCalculator.Domain.Pricing;

public sealed class PerKgPricingStrategy : IPricingStrategy
{
    public decimal CalculatePrice(
        decimal basePrice,
        decimal quantity)
    {
        return basePrice * quantity;
    }
}