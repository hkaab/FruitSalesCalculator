using FruitSalesCalculator.Domain.Models;

namespace FruitSalesCalculator.Domain.Pricing;

public sealed class PerKgPricingStrategy : IPricingStrategy
{
    public decimal CalculatePrice(
        decimal basePrice,
        decimal quantity,
        OrderContext context)
    {
        return basePrice * quantity;
    }
}