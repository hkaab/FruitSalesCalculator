using System;
using System.Collections.Generic;
using System.Text;

namespace FruitSalesCalculator.Domain.Pricing
{
    public class BulkDiscounrDecorator : IPricingStrategy
    {
        private readonly IPricingStrategy _innerStrategy;
        public BulkDiscounrDecorator(IPricingStrategy innerStrategy)
        {
            _innerStrategy = innerStrategy;
        }
        public decimal CalculatePrice(decimal basePrice, decimal quantity)
        {
            var price = _innerStrategy.CalculatePrice(basePrice, quantity);
            if (quantity >= 10)
            {
                price *= 0.9m; // Apply a 10% discount for bulk purchases
            }
            return price;
        }
    }
}
