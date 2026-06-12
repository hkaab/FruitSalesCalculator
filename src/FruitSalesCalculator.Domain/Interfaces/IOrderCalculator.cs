using FruitSalesCalculator.Domain.Models;

namespace FruitSalesCalculator.Domain.Interfaces;

public interface IOrderCalculator
{
    decimal CalculateTotal(Order order);
}