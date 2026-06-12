namespace FruitSalesCalculator.Domain.Pricing;

public interface IPricingStrategy
{
    decimal CalculatePrice(
        decimal basePrice,
        decimal quantity);
}