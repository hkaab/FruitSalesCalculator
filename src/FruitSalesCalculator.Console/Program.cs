using FruitSalesCalculator.Console.Application;
using FruitSalesCalculator.Domain.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddFruitSalesCalculator();

services.AddTransient<ConsoleApplication>();

var serviceProvider =
    services.BuildServiceProvider();
Console.WriteLine("Starting Console Application...");
var app =
    serviceProvider.GetRequiredService<ConsoleApplication>();
app.Run();