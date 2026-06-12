using FruitSalesCalculator.Domain.Interfaces;
using FruitSalesCalculator.Domain.Models;

namespace FruitSalesCalculator.Console.Application;

public sealed class ConsoleApplication
{
    private readonly IFruitCatalog _fruitCatalog;
    private readonly IOrderCalculator _orderCalculator;

    public ConsoleApplication(
        IFruitCatalog fruitCatalog,
        IOrderCalculator orderCalculator)
    {
        _fruitCatalog = fruitCatalog;
        _orderCalculator = orderCalculator;
    }

    public void Run()
    {
        var order = BuildSampleOrder();

        DisplayOrder(order);

        var total =
            _orderCalculator.CalculateTotal(order);

        System.Console.WriteLine();
        System.Console.WriteLine($"Total Cost: ${total:F2}");
    }

    private Order BuildSampleOrder()
    {
        var order = new Order();

        order.AddItem(
            _fruitCatalog.GetByName("Apple"),
            3);

        order.AddItem(
            _fruitCatalog.GetByName("Banana"),
            10);

        order.AddItem(
            _fruitCatalog.GetByName("Cherry"),
            3);

        return order;
    }

    private static void DisplayOrder(
        Order order)
    {
        System.Console.WriteLine("Fruit Sales Calculator");
        System.Console.WriteLine("======================");
        System.Console.WriteLine();

        foreach (var item in order.Items)
        {
            var price =
                item.Fruit.PricingStrategy.CalculatePrice(
                    item.Fruit.BasePrice,
                    item.Quantity);

            System.Console.WriteLine(
                $"{item.Fruit.Name,-10} Qty: {item.Quantity,-5} Price: ${price:F2}");
        }
    }
}