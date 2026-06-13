# Fruit Sales Calculator

![Build](https://github.com/hkaab/FruitSalesCalculator/actions/workflows/ci.yml/badge.svg)
![Build](https://github.com/hkaab/FruitSalesCalculator/actions/workflows/ut.yml/badge.svg)

A sample fruit pricing engine built with **.NET 10**, demonstrating:

- SOLID Principles
- Strategy Pattern
- Dependency Injection
- Unit Testing with xUnit
- ASP.NET Core Web API
- Console Application
- GitHub Actions CI

---

# Overview

This application calculates the total cost of fruit orders using configurable pricing strategies.

The solution was built to satisfy the coding exercise requirements while demonstrating clean architecture principles and extensibility.

Supported pricing models:

- Per Kilogram
- Per Item
- Discounted Per Kilogram

Example:

| Fruit | Pricing |
|---------|---------|
| Apple | $2.00 per kg |
| Banana | $0.30 per item |
| Cherry | $5.00 per kg with 10% discount above 2kg |

---

# Solution Structure

```text
FruitSalesCalculator.sln

src/
├── FruitSalesCalculator.Domain
├── FruitSalesCalculator.Api
└── FruitSalesCalculator.Console

tests/
└── FruitSalesCalculator.Tests

.github/
└── workflows
    └── ci.yml

README.md
global.json
Directory.Packages.props
```

---

# Design Decisions

The solution is separated into multiple projects to demonstrate proper separation of concerns.

## Domain

Contains all business logic.

Responsibilities:

- Fruit definitions
- Orders
- Pricing calculations
- Catalog management
- Business services

No UI, Console, or API concerns exist in this layer.

---

## API

ASP.NET Core Web API exposing the pricing engine through HTTP endpoints.

Responsibilities:

- Request validation
- DTOs
- HTTP endpoints
- Swagger documentation

Contains no pricing logic.

---

## Console

Simple console application demonstrating how the pricing engine can be consumed by another application.

Contains no pricing logic.

---

## Tests

Contains unit tests validating:

- Pricing calculations
- Order totals
- Catalog behavior
- Controller behavior
- Validation rules

---

# Design Pattern

## Strategy Pattern

Pricing behavior is encapsulated inside pricing strategy implementations.

```csharp
public interface IPricingStrategy
{
    decimal CalculatePrice(
        decimal basePrice,
        decimal quantity);
}
```

Implementations:

```text
PerKgPricingStrategy

PerItemPricingStrategy

DiscountedKgPricingStrategy
```

### Why Strategy Pattern?

Different fruits can use different pricing models.

Instead of adding:

```csharp
switch(fruitType)
{
}
```

or

```csharp
if(...)
{
}
```

pricing behavior is delegated to strategy implementations.

This makes the solution:

- Easier to maintain
- Easier to test
- Easier to extend

---

## Example Extension

Adding a new pricing model requires only a new strategy implementation.

```csharp
public sealed class BuyOneGetOneFreeStrategy
    : IPricingStrategy
{
    public decimal CalculatePrice(
        decimal basePrice,
        decimal quantity)
    {
        var payableItems =
            Math.Ceiling(quantity / 2m);

        return payableItems * basePrice;
    }
}
```

No existing code needs to be modified.

---

# SOLID Principles

## Single Responsibility Principle

Each class has a single responsibility.

Examples:

```text
Fruit
    → Fruit data

OrderCalculator
    → Order total calculations

InMemoryFruitCatalog
    → Fruit lookup

PerKgPricingStrategy
    → Per kilogram pricing
```

---

## Open Closed Principle

The system is open for extension but closed for modification.

New pricing models can be added by implementing:

```csharp
IPricingStrategy
```

without changing existing code.

---

## Liskov Substitution Principle

All pricing strategies are interchangeable.

```csharp
IPricingStrategy strategy =
    new PerKgPricingStrategy();
```

can be replaced with:

```csharp
IPricingStrategy strategy =
    new DiscountedKgPricingStrategy(...);
```

without breaking consumers.

---

## Interface Segregation Principle

Small focused interfaces:

```csharp
IFruitCatalog

IOrderCalculator
```

Clients only depend on functionality they actually need.

---

## Dependency Inversion Principle

Application services depend on abstractions.

```csharp
IFruitCatalog

IOrderCalculator
```

instead of concrete implementations.

---

# Supported Fruits

## Apple

Price:

```text
$2.00 per kg
```

Pricing Strategy:

```text
PerKgPricingStrategy
```

---

## Banana

Price:

```text
$0.30 per item
```

Pricing Strategy:

```text
PerItemPricingStrategy
```

---

## Cherry

Price:

```text
$5.00 per kg
```

Discount:

```text
10% discount for orders above 2kg
```

Pricing Strategy:

```text
DiscountedKgPricingStrategy
```

---

# Example Calculation

Order:

```text
Apple   3kg
Banana  10 items
Cherry  3kg
```

Calculation:

```text
Apple

3 × $2.00
=
$6.00

Banana

10 × $0.30
=
$3.00

Cherry

3 × $5.00
=
$15.00

10% Discount
=
$13.50
```

Total:

```text
$22.50
```

---

# Prerequisites

Install:

- .NET 10 SDK

Verify installation:

```bash
dotnet --version
```

Expected:

```text
10.0.100
```

(or later)

---

# Clone Repository

```bash
git clone https://github.com/hkaab/FruitSalesCalculator.git

cd FruitSalesCalculator
```

---

# Restore Dependencies

```bash
dotnet restore
```

---

# Build Solution

```bash
dotnet build
```

Expected output:

```text
Build succeeded.
```

---

# Run Unit Tests

```bash
dotnet test
```

Expected output:

```text
Passed! All tests passed.
```

---

# Running the Console Application

The Console application demonstrates the pricing engine with a sample order.

Run:

```bash
dotnet run --project src/FruitSalesCalculator.Console
```

Expected output:

```text
Fruit Sales Calculator
======================

Apple      Qty: 3     Price: $6.00
Banana     Qty: 10    Price: $3.00
Cherry     Qty: 3     Price: $13.50

Total Cost: $22.50
```

---

# Running the Web API

Run:

```bash
dotnet run --project src/FruitSalesCalculator.Api
```

Expected output:

```text
Now listening on:
https://localhost:7xxx
```

---

# Swagger

Navigate to:

```text
https://localhost:7xxx/swagger
```

Swagger UI provides interactive API documentation.

---

# API Endpoints

## Calculate Order

### Request

```http
POST /api/orders/calculate
```

Request body:

```json
{
  "items": [
    {
      "fruitName": "Apple",
      "quantity": 3
    },
    {
      "fruitName": "Banana",
      "quantity": 10
    },
    {
      "fruitName": "Cherry",
      "quantity": 3
    }
  ]
}
```

### Response

```json
{
  "total": 22.5
}
```

---

# Running in Visual Studio

1. Open:

```text
FruitSalesCalculator.sln
```

2. Set startup project:

```text
FruitSalesCalculator.Api
```

or

```text
FruitSalesCalculator.Console
```

3. Press:

```text
F5
```

to run with debugging.

---

# Continuous Integration

GitHub Actions automatically performs:

- Restore
- Build
- Test

for every:

- Push
- Pull Request

Workflow file:

```text
.github/workflows/ci.yml
```

---

# Test Coverage

The solution includes tests for:

### Pricing

- PerKgPricingStrategy
- PerItemPricingStrategy
- DiscountedKgPricingStrategy

### Domain

- Fruit validation
- OrderItem validation
- Order calculation

### Catalog

- Fruit lookup
- Unknown fruit handling
- Case-insensitive lookup

### API

- Successful order calculation
- Invalid fruit handling

Coverage includes:

```text
✓ Pricing calculations

✓ Validation rules

✓ Order totals

✓ Catalog behavior

✓ Controller behavior

✓ Error handling
```

---

# Future Enhancements

Potential future additions:

- Buy One Get One Free
- Seasonal Discounts
- Loyalty Pricing
- Percentage Promotions
- Inventory Tracking
- Database-backed Catalog
- Persistence Layer
- Product Management API

All of these can be implemented with minimal impact because pricing behavior is isolated through the Strategy Pattern.

---

# Author Notes

This solution was intentionally designed to balance:

- Simplicity
- Maintainability
- Extensibility
- Testability

while avoiding unnecessary complexity for the scope of the exercise.

The architecture demonstrates clean separation of concerns and provides a solid foundation for future enhancements while remaining easy to understand and discuss during a technical interview.
