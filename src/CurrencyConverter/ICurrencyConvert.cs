namespace CurrencyConverter
{
    public interface ICurrencyConvert
    {
        Money Convert(Money from, Currency to);
    }
}