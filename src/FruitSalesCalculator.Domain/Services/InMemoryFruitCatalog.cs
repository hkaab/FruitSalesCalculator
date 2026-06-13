using FruitSalesCalculator.Domain.Interfaces;
using FruitSalesCalculator.Domain.Models;
using FruitSalesCalculator.Domain.Pricing;

namespace FruitSalesCalculator.Domain.Services;

public sealed class InMemoryFruitCatalog : IFruitCatalog
{
    private readonly Dictionary<string, Fruit> _catalog;

    public InMemoryFruitCatalog()
    {
        _catalog = new Dictionary<string, Fruit>(
            StringComparer.OrdinalIgnoreCase)
        {
            {
                "Apple",
                new Fruit(
                    "Apple",
                    2.00m,
                    new PerKgPricingStrategy())
            },
            {
                "Banana",
                new Fruit(
                    "Banana",
                    0.30m,
                    new PerItemPricingStrategy())
            },
            {
                "Cherry",
                new Fruit(
                    "Cherry",
                    5.00m,
                    new DiscountedKgPricingStrategy(
                        thresholdKg: 2,
                        discountPercentage: 10))
            },
            {
                "Date",
                new Fruit(
                    "Date",
                    12.00m,
                    new BulkDiscountDecorator(new PerKgPricingStrategy ()))
            }
        };
    }

    public Fruit GetByName(string fruitName)
    {
        if (!_catalog.TryGetValue(
                fruitName,
                out var fruit))
        {
            throw new KeyNotFoundException(
                $"Fruit '{fruitName}' was not found.");
        }

        return fruit;
    }
}