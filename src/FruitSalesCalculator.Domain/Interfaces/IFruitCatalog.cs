using FruitSalesCalculator.Domain.Models;

namespace FruitSalesCalculator.Domain.Interfaces;

public interface IFruitCatalog
{
    Fruit GetByName(string fruitName);
}