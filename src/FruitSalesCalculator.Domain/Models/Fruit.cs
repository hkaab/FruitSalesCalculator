using FruitSalesCalculator.Domain.Pricing;

namespace FruitSalesCalculator.Domain.Models;

public sealed class Fruit
{
    public string Name { get; }

    public decimal BasePrice { get; }

    public IPricingStrategy PricingStrategy { get; }

    public Fruit(
        string name,
        decimal basePrice,
        IPricingStrategy pricingStrategy)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Fruit name cannot be empty.",
                nameof(name));
        }

        if (basePrice <= 0)
        {
            throw new ArgumentException(
                "Base price must be greater than zero.",
                nameof(basePrice));
        }

        PricingStrategy = pricingStrategy
            ?? throw new ArgumentNullException(nameof(pricingStrategy));

        Name = name;
        BasePrice = basePrice;
    }
}