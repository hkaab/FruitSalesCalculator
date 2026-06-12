using FruitSalesCalculator.Api.Contracts;
using FruitSalesCalculator.Domain.Interfaces;
using FruitSalesCalculator.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FruitSalesCalculator.Api.Controllers;

[ApiController]
[Route("api/orders")]
[Produces("application/json")]
public sealed class OrdersController : ControllerBase
{
    private readonly IFruitCatalog _fruitCatalog;
    private readonly IOrderCalculator _orderCalculator;

    public OrdersController(
        IFruitCatalog fruitCatalog,
        IOrderCalculator orderCalculator)
    {
        _fruitCatalog = fruitCatalog;
        _orderCalculator = orderCalculator;
    }

    [HttpPost("calculate")]
    [ProducesResponseType(
        typeof(CalculateOrderResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        typeof(ErrorResponse),
        StatusCodes.Status400BadRequest)]
    public ActionResult<CalculateOrderResponse> Calculate(
        [FromBody] CalculateOrderRequest request)
    {
        try
        {
            var order = new Order();

            foreach (var item in request.Items)
            {
                var fruit = _fruitCatalog.GetByName(
                    item.FruitName);

                order.AddItem(
                    fruit,
                    item.Quantity);
            }

            var total = _orderCalculator.CalculateTotal(
                order);

            return Ok(
                new CalculateOrderResponse
                {
                    Total = total
                });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(
                new ErrorResponse
                {
                    Message = ex.Message
                });
        }
    }
}