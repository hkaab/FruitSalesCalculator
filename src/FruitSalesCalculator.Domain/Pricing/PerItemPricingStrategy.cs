namespace FruitSalesCalculator.Domain.Pricing;

public sealed class PerItemPricingStrategy : IPricingStrategy
{
    public decimal CalculatePrice(
        decimal basePrice,
        decimal quantity)
    {
        return basePrice * quantity;
    }
}