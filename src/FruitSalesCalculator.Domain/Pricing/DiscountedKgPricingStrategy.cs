namespace FruitSalesCalculator.Domain.Pricing;

public sealed class DiscountedKgPricingStrategy : IPricingStrategy
{
    private readonly decimal _thresholdKg;
    private readonly decimal _discountPercentage;

    public DiscountedKgPricingStrategy(
        decimal thresholdKg,
        decimal discountPercentage)
    {
        if (thresholdKg <= 0)
        {
            throw new ArgumentException(
                "Threshold must be greater than zero.",
                nameof(thresholdKg));
        }

        if (discountPercentage <= 0 ||
            discountPercentage > 100)
        {
            throw new ArgumentException(
                "Discount percentage must be between 0 and 100.",
                nameof(discountPercentage));
        }

        _thresholdKg = thresholdKg;
        _discountPercentage = discountPercentage;
    }

    public decimal CalculatePrice(
        decimal basePrice,
        decimal quantity)
    {
        var total = basePrice * quantity;

        if (quantity <= _thresholdKg)
        {
            return total;
        }

        return total * (1 - (_discountPercentage / 100m));
    }
}