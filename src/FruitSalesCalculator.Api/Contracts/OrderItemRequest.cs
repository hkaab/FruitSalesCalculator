using System.ComponentModel.DataAnnotations;

namespace FruitSalesCalculator.Api.Contracts;

public sealed class OrderItemRequest
{
    [Required]
    public string FruitName { get; init; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Quantity { get; init; }
}