# Fruit Sales Calculator

![Build](https://github.com/hkaab/FruitSalesCalculator/actions/workflows/ci.yml/badge.svg)
![Build](https://github.com/hkaab/FruitSalesCalculator/actions/workflows/ut.yml/badge.svg)

A simple fruit pricing engine built with .NET 8 demonstrating SOLID principles, Strategy Pattern implementation, unit testing, dependency injection, and multiple presentation layers (Console and Web API).

---

## Overview

This application calculates the total cost of fruit orders using configurable pricing strategies.

Supported pricing models:

- Per Kilogram
- Per Item
- Discounted Per Kilogram

The solution was intentionally designed to be extensible so new fruit types, promotions, and pricing models can be added without modifying existing business logic.

---

## Architecture

```text
FruitSalesCalculator.sln

src/
├── FruitSalesCalculator.Domain
├── FruitSalesCalculator.Api
└── FruitSalesCalculator.Console

tests/
└── FruitSalesCalculator.Tests
```

### Domain

Contains:

- Business entities
- Pricing strategies
- Domain services
- Interfaces

### API

ASP.NET Core Web API exposing pricing functionality.

### Console

Simple console application demonstrating usage.

### Tests

xUnit test suite covering domain and API behavior.

---

## Design Pattern

### Strategy Pattern

Pricing calculations are encapsulated behind:

```csharp
IPricingStrategy
```

Implementations:

```csharp
PerKgPricingStrategy

PerItemPricingStrategy

DiscountedKgPricingStrategy
```

This enables adding new pricing methods without modifying existing code.

Example:

```csharp
public sealed class BuyOneGetOneFreeStrategy
    : IPricingStrategy
{
}
```

---

## SOLID Principles

### Single Responsibility Principle

Each class has one responsibility.

Examples:

- Fruit → Fruit information
- OrderCalculator → Order calculations
- InMemoryFruitCatalog → Fruit lookup
- Pricing strategies → Price calculations

---

### Open Closed Principle

New pricing strategies can be added without modifying existing code.

```csharp
public sealed class LoyaltyDiscountStrategy
    : IPricingStrategy
{
}
```

---

### Liskov Substitution Principle

All pricing strategies can be substituted through:

```csharp
IPricingStrategy
```

---

### Interface Segregation Principle

Small focused interfaces:

```csharp
IFruitCatalog

IOrderCalculator
```

---

### Dependency Inversion Principle

Application services depend on abstractions rather than concrete implementations.

```csharp
IFruitCatalog

IOrderCalculator
```

---

## Supported Fruits

### Apple

Price:

```text
$2.00 per kg
```

Pricing Strategy:

```text
PerKgPricingStrategy
```

---

### Banana

Price:

```text
$0.30 per item
```

Pricing Strategy:

```text
PerItemPricingStrategy
```

---

### Cherry

Price:

```text
$5.00 per kg
```

Discount:

```text
10% discount for orders greater than 2kg
```

Pricing Strategy:

```text
DiscountedKgPricingStrategy
```

---

## Example Calculation

Order:

```text
Apple   3kg
Banana  10 items
Cherry  3kg
```

Calculation:

```text
Apple

3 x $2.00
=
$6.00

Banana

10 x $0.30
=
$3.00

Cherry

3 x $5.00
=
$15.00

10% discount
=
$13.50
```

Total:

```text
$22.50
```

---

## API

### Calculate Order

Endpoint:

```http
POST /api/orders/calculate
```

Request:

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

Response:

```json
{
  "total": 22.5
}
```

---

## Running the Solution

### Restore Packages

```bash
dotnet restore
```

### Build

```bash
dotnet build
```

### Run Tests

```bash
dotnet test
```

---

## Run Console Application

```bash
dotnet run --project src/FruitSalesCalculator.Console
```

Example output:

```text
Fruit Sales Calculator
======================

Apple      Qty: 3     Price: $6.00
Banana     Qty: 10    Price: $3.00
Cherry     Qty: 3     Price: $13.50

Total Cost: $22.50
```

---

## Run API

```bash
dotnet run --project src/FruitSalesCalculator.Api
```

Swagger:

```text
https://localhost:5001/swagger
```

---

## Testing

The solution includes tests for:

- Fruit validation
- OrderItem validation
- Pricing strategies
- Order calculation
- Fruit catalog
- API controller

Coverage includes:

```text
✓ PerKgPricingStrategy

✓ PerItemPricingStrategy

✓ DiscountedKgPricingStrategy

✓ Fruit constructor validation

✓ OrderItem validation

✓ Fruit catalog

✓ Order calculation

✓ API controller behavior

✓ Error handling
```

---

## Future Enhancements

Potential additions:

- Buy One Get One Free
- Seasonal Pricing
- Loyalty Discounts
- Percentage Promotions
- Database-backed Fruit Catalog
- Inventory Management
- Persistence Layer

All can be added with minimal impact due to the Strategy Pattern and SOLID architecture.

---

## Author Notes

This solution was intentionally designed to balance:

- Simplicity
- Maintainability
- Extensibility
- Testability
