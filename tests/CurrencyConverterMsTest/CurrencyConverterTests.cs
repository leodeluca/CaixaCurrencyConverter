using CurrencyConverter;

namespace CurrencyConverterMsTest
{
    // TAREFA
    // Testar Conversão A -> B
    // Testar Conversão A -> B, B -> A (colocar na tabela A->B e B->A)

    [TestClass]
    public class CurrencyConverterTests
    {

        [TestMethod]
        public void GivenOneCurrency_WhenConverting_ThenReturnASecondCurrency()
        {
            // Arrange
            var amount = 5.45m;
            Money money = new(1m, Currency.USD);
            var rates = new Dictionary<(Currency From, Currency To), decimal> { { (Currency.USD, Currency.BRL), amount } };
            var rateprovider = new RateProvider(rates);
            var sut = new CurrencyConvert(rateprovider);

            // Act
            var result = sut.Convert(money, Currency.BRL);

            // Assert
            Assert.AreEqual(result, new Money(amount, Currency.BRL));
        }

        [TestMethod]
        public void GivenACurrency_WhenConvertingOnTheContraryWay_ThenReturnTheSameCurrencyValue()
        {
            // Arrange
            var amount_usd = 1.00m;
            var amount_brl = 5.397m;

            var brl_usd = 5.397m;
            var usd_brl = 0.185295m;

            Money money_usd = new(amount_usd, Currency.USD);
            Money money_brl = new(amount_brl, Currency.BRL);

            var rates_usd = new Dictionary<(Currency From, Currency To), decimal> { { (Currency.USD, Currency.BRL), brl_usd } };
            var rateprovider_usd = new RateProvider(rates_usd);

            var rates_brl = new Dictionary<(Currency From, Currency To), decimal> { { (Currency.BRL, Currency.USD), usd_brl } };
            var rateprovider_brl = new RateProvider(rates_brl);

            var sut_usd = new CurrencyConvert(rateprovider_usd);
            var sut_brl = new CurrencyConvert(rateprovider_brl);

            // Act
            var result_usd = sut_usd.Convert(money_usd, Currency.BRL);
            var result_amount_usd = result_usd.Amount;

            var result_brl = sut_brl.Convert(money_brl, Currency.USD);
            var result_amount_brl = result_brl.Amount;

            // Assert
            Assert.AreEqual(Math.Round(result_amount_usd,3), amount_brl);
            Assert.AreEqual(Math.Round(result_amount_brl,3), amount_usd);
        }

    }
}
