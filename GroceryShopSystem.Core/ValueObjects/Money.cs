namespace GroceryShopSystem.Core.ValueObjects;

// Remove parameters from the record declaration to avoid generating a duplicate constructor
public record Money
{
    // 1. Define properties explicitly
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    // 2. Keep your custom constructor with validation logic
    public Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentException("Amount cannot be negative");
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required");

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    public static Money Zero(string currency) => new Money(0, currency);

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Currencies must match");

        return new Money(Amount + other.Amount, Currency);
    }
}