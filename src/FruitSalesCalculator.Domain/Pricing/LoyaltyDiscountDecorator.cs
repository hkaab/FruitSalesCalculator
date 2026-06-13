using FruitSalesCalculator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitSalesCalculator.Domain.Pricing
{
    public class LoyaltyDiscountDecorator: IPricingStrategy
    {
        private readonly IPricingStrategy _innerStrategy;
        public LoyaltyDiscountDecorator(IPricingStrategy innerStrategy )
        {
            _innerStrategy = innerStrategy;
        }
        public decimal CalculatePrice(decimal basePrice, decimal quantity, OrderContext context)
        {
            var price = _innerStrategy.CalculatePrice(basePrice, quantity, context);
            if (context.IsLoyalCustomer)
                price *= 0.95m; // Apply a 5% loyalty discount
            return price;
        }
    }
}
