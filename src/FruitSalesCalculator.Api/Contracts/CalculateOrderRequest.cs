using System.ComponentModel.DataAnnotations;

namespace FruitSalesCalculator.Api.Contracts;

public sealed class CalculateOrderRequest
{
    [Required]
    [MinLength(1)]
    public IReadOnlyCollection<OrderItemRequest> Items { get; init; }
        = [];
}