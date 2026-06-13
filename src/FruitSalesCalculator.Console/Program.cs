using FruitSalesCalculator.Console.Application;
using FruitSalesCalculator.Domain.Interfaces;
using FruitSalesCalculator.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IFruitCatalog, InMemoryFruitCatalog>();
services.AddScoped<IOrderCalculator, OrderCalculator>();

services.AddTransient<ConsoleApplication>();

var serviceProvider =
    services.BuildServiceProvider();
Console.WriteLine("Starting Console Application...");
var app =
    serviceProvider.GetRequiredService<ConsoleApplication>();
app.Run();