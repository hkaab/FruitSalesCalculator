using FruitSalesCalculator.Api.Contracts;
using FruitSalesCalculator.Api.Controllers;
using FruitSalesCalculator.Domain.Interfaces;
using FruitSalesCalculator.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FruitSalesCalculator.Tests.Api;

public class OrdersControllerTests
{
    [Fact]
    public void Calculate_ShouldReturnCorrectTotal()
    {
        // Arrange

        IFruitCatalog catalog =
            new InMemoryFruitCatalog();

        IOrderCalculator calculator =
            new OrderCalculator();

        var controller =
            new OrdersController(
                catalog,
                calculator);

        var request =
            new CalculateOrderRequest
            {
                Items =
                [
                    new OrderItemRequest
                    {
                        FruitName = "Apple",
                        Quantity = 3
                    }
                ]
            };

        // Act

        var result =
            controller.Calculate(request);

        // Assert

        var okResult =
            Assert.IsInstanceOfType<OkObjectResult>(
                result.Result);

        var response =
            Assert.IsInstanceOfType<CalculateOrderResponse>(
                okResult.Value);

        Assert.AreEqual(6m, response.Total);
    }

    [Fact]
    public void Calculate_ShouldReturnBadRequest_WhenFruitNotFound()
    {
        // Arrange

        IFruitCatalog catalog =
            new InMemoryFruitCatalog();

        IOrderCalculator calculator =
            new OrderCalculator();

        var controller =
            new OrdersController(
                catalog,
                calculator);

        var request =
            new CalculateOrderRequest
            {
                Items =
                [
                    new OrderItemRequest
                    {
                        FruitName = "Orange",
                        Quantity = 1
                    }
                ]
            };

        // Act

        var result =
            controller.Calculate(request);

        // Assert

        Assert.IsInstanceOfType<BadRequestObjectResult>(
            result.Result);
    }
}