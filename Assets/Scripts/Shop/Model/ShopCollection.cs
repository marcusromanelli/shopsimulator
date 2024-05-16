using System.Collections.Generic;
using UnityEngine;
using static ShopController;

[CreateAssetMenu(menuName = "Shop/Collection")]
public class ShopCollection : ScriptableObject
{
    [SerializeField] ShopSeller seller;
    [SerializeField] ShopItem[] items;

    private List<CurrencyData> acceptedCurrenciesCache;

    public ShopSeller GetSeller() => seller;
    public ShopItem[] GetItems() => items;

    public List<CurrencyData> GetAcceptedCurrencies()
    {
        if (acceptedCurrenciesCache == null || acceptedCurrenciesCache.Count == 0)
            LoadAcceptedCurrencies();

        return acceptedCurrenciesCache;
    }

    void LoadAcceptedCurrencies()
    {
        acceptedCurrenciesCache = new List<CurrencyData>();

        foreach (var item in items)
        {
            var currency = item.GetCostData().currency;

            if (!acceptedCurrenciesCache.Contains(currency))
                acceptedCurrenciesCache.Add(currency);
        }
    }
}
