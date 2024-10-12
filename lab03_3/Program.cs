using System;

public abstract class Currency
{
    public decimal Value { get; set; }

    protected Currency(decimal value)
    {
        Value = value;
    }
}

public class CurrencyUSD : Currency
{
    public decimal ExchangeRate { get; set; } // Курс обмена для USD

    public CurrencyUSD(decimal value, decimal exchangeRate) : base(value)
    {
        ExchangeRate = exchangeRate;
    }

    // Явное преобразование CurrencyUSD в CurrencyEUR
    public static explicit operator CurrencyEUR(CurrencyUSD usd)
    {
        return new CurrencyEUR(usd.Value * usd.ExchangeRate, usd.ExchangeRate);
    }

    // Явное преобразование CurrencyUSD в CurrencyRUB
    public static explicit operator CurrencyRUB(CurrencyUSD usd)
    {
        return new CurrencyRUB(usd.Value * usd.ExchangeRate, usd.ExchangeRate);
    }
}

public class CurrencyEUR : Currency
{
    public decimal ExchangeRate { get; set; } // Курс обмена для EUR

    public CurrencyEUR(decimal value, decimal exchangeRate) : base(value)
    {
        ExchangeRate = exchangeRate;
    }

    // Явное преобразование CurrencyEUR в CurrencyUSD
    public static explicit operator CurrencyUSD(CurrencyEUR eur)
    {
        return new CurrencyUSD(eur.Value / eur.ExchangeRate, eur.ExchangeRate);
    }

    // Явное преобразование CurrencyEUR в CurrencyRUB
    public static explicit operator CurrencyRUB(CurrencyEUR eur)
    {
        return new CurrencyRUB(eur.Value * eur.ExchangeRate, eur.ExchangeRate);
    }
}

public class CurrencyRUB : Currency
{
    public decimal ExchangeRate { get; set; } // Курс обмена для RUB

    public CurrencyRUB(decimal value, decimal exchangeRate) : base(value)
    {
        ExchangeRate = exchangeRate;
    }

    // Явное преобразование CurrencyRUB в CurrencyUSD
    public static explicit operator CurrencyUSD(CurrencyRUB rub)
    {
        return new CurrencyUSD(rub.Value / rub.ExchangeRate, rub.ExchangeRate);
    }

    // Явное преобразование CurrencyRUB в CurrencyEUR
    public static explicit operator CurrencyEUR(CurrencyRUB rub)
    {
        return new CurrencyEUR(rub.Value / rub.ExchangeRate, rub.ExchangeRate);
    }
}

class Program
{
    static void Main()
    {
        // Текущие обменные курсы (примерные)
        decimal usdToEurRate = 0.85m; // 1 USD = 0.85 EUR
        decimal usdToRubRate = 75m;   // 1 USD = 75 RUB

        // Создаем объект CurrencyUSD
        CurrencyUSD usd = new CurrencyUSD(100, usdToEurRate);

        // Преобразуем USD в EUR
        CurrencyEUR eur = (CurrencyEUR)usd;
        Console.WriteLine($"100 USD in EUR: {eur.Value}");

        // Преобразуем USD в RUB
        CurrencyRUB rub = (CurrencyRUB)usd;
        Console.WriteLine($"100 USD in RUB: {rub.Value}");

        // Преобразуем EUR обратно в USD
        CurrencyUSD usdFromEur = (CurrencyUSD)eur;
        Console.WriteLine($"{eur.Value} EUR in USD: {usdFromEur.Value}");

        // Преобразуем RUB обратно в USD
        CurrencyUSD usdFromRub = (CurrencyUSD)rub;
        Console.WriteLine($"{rub.Value} RUB in USD: {usdFromRub.Value}");
    }
}
