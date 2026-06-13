using FruitSalesCalculator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitSalesCalculator.Domain.Pricing
{
    public class BulkDiscountDecorator : IPricingStrategy
    {
        private readonly IPricingStrategy _innerStrategy;
        public BulkDiscountDecorator(IPricingStrategy innerStrategy)
        {
            _innerStrategy = innerStrategy;
        }
        public decimal CalculatePrice(decimal basePrice, decimal quantity, OrderContext context)
        {
            var price = _innerStrategy.CalculatePrice(basePrice, quantity, context);
            if (quantity >= 10)
            {
                price *= 0.9m; // Apply a 10% discount for bulk purchases
            }
            return price;
        }
    }
}
