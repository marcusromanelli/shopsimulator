using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static ShopItem;

public class PlayerInventory : MonoBehaviour
{
    public UnityEvent<CurrencyData, int> OnCurrencyChanges;

    private Dictionary<string, int> currencyList;
    private List<string> itemList;

    private Dictionary<string, CurrencyData> cachedCurrencyData = new Dictionary<string, CurrencyData>();

    public void AddItem(string guid)
    {
        itemList.Add(guid);
    }

    public void RemoveItem(string guid)
    {
        itemList.Remove(guid);
    }

    public List<string> GetItemList()
    {
        return itemList;
    }


    public void AddCurrency(string currencyId, int amount)
    {
        var current = GetCurrencyAmount(currencyId);

        current += amount;

        SetCurrency(currencyId, current);

        var currencyData = GetCurrencyData(currencyId);

        OnCurrencyChanges?.Invoke(currencyData, amount);
    }
    public void RemoveCurrency(string currencyId, int amount)
    {
        var current = GetCurrencyAmount(currencyId);

        current -= amount;

        SetCurrency(currencyId, current);

        var currencyData = GetCurrencyData(currencyId);

        OnCurrencyChanges?.Invoke(currencyData, amount);
    }

    public bool HasCurrency(string currencyId, int value)
    {
        return GetCurrencyAmount(currencyId) >= value;
    }


    void SetCurrency(string currencyId, int value)
    {
        if(currencyList == null)
            currencyList = new Dictionary<string, int>();

        if (value < 0)
            value = 0;

        currencyList[currencyId] = value;
    }

    public int GetCurrencyAmount(string currencyId)
    {
        if (currencyList == null || !currencyList.ContainsKey(currencyId))
            SetCurrency(currencyId, 0);

        return currencyList[currencyId];
    }
    public int GetCurrencyAmount(Cost costData)
    {
        return GetCurrencyAmount(costData.currency.GetId());
    }

    CurrencyData GetCurrencyData(string currencyId)
    {
        if (!cachedCurrencyData.ContainsKey(currencyId))
            cachedCurrencyData[currencyId] = CurrencyManager.Instance.GetCurrencyData(currencyId);

        return cachedCurrencyData[currencyId];
    }
}
