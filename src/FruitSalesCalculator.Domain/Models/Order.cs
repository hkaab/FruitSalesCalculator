namespace FruitSalesCalculator.Domain.Models;

public sealed class Order
{
    private readonly List<OrderItem> _items = [];

    public IReadOnlyCollection<OrderItem> Items => _items;

    public void AddItem(
        Fruit fruit,
        decimal quantity)
    {
        _items.Add(
            new OrderItem(
                fruit,
                quantity));
    }
}