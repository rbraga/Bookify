namespace Bookify.Domain.Appartments;

public record Money(decimal Amount, Currency currency)
{
    public static Money operator +(Money first, Money second)
    {
        if (first.currency != second.currency)
        {
            throw new InvalidOperationException("Correncies have to be equal");
        }

        return new Money(first.Amount + second.Amount, first.currency);
    }

    public static Money Zero() => new(0, Currency.None);
}
