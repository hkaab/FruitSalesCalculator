using FruitSalesCalculator.Domain.Interfaces;
using FruitSalesCalculator.Domain.Models;

namespace FruitSalesCalculator.Domain.Services;

public sealed class OrderCalculator : IOrderCalculator
{
    public decimal CalculateTotal(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        return order.Items.Sum(item =>
            item.Fruit.PricingStrategy.CalculatePrice(
                item.Fruit.BasePrice,
                item.Quantity));
    }
}