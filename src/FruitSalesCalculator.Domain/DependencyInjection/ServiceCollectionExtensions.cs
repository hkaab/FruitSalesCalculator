using FruitSalesCalculator.Domain.Interfaces;
using FruitSalesCalculator.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FruitSalesCalculator.Domain.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFruitSalesCalculator(
        this IServiceCollection services)
    {
        services.AddSingleton<IFruitCatalog, InMemoryFruitCatalog>();

        services.AddScoped<IOrderCalculator, OrderCalculator>();

        return services;
    }
}