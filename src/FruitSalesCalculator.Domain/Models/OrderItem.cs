namespace FruitSalesCalculator.Domain.Models;

public sealed class OrderItem
{
    public Fruit Fruit { get; }

    public decimal Quantity { get; }

    public OrderItem(
        Fruit fruit,
        decimal quantity)
    {
        Fruit = fruit
            ?? throw new ArgumentNullException(nameof(fruit));

        if (quantity <= 0)
        {
            throw new ArgumentException(
                "Quantity must be greater than zero.",
                nameof(quantity));
        }

        Quantity = quantity;
    }
}