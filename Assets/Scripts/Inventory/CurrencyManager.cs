using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    
    [SerializeField] List<CurrencyData> Currencies;

    public List<CurrencyData> GetCurrencies() => Currencies;

    public CurrencyData GetCurrencyData(string currencyId)
    {
        foreach(var currency in Currencies)
            if(currency.GetId().Equals(currencyId))
                return currency;

        return null;
    }
}
