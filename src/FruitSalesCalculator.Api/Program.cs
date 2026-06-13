
using FruitSalesCalculator.Api.Middleware;
using FruitSalesCalculator.Domain.Interfaces;
using FruitSalesCalculator.Domain.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddSingleton<IFruitCatalog, InMemoryFruitCatalog>();

builder.Services.AddScoped<IOrderCalculator, OrderCalculator>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{

    // Map OpenAPI JSON endpoint
    app.MapOpenApi();

    // Enable Swagger UI
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
