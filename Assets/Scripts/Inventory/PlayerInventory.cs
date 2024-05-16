using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static ShopItem;

public class PlayerInventory : MonoBehaviour
{
    [Serializable]
    public struct InventorySlot
    {
        public string itemId;
    }
    [Serializable]
    public struct InventoryData
    {
        public InventorySlot Accessory;
        public InventorySlot Shirt;
        public InventorySlot Hair;
    }

    public UnityEvent<CurrencyData, int> OnCurrencyChanges;

    [SerializeField] private AccessoryAnimation accessAnimation;

    private Dictionary<string, int> currencyList;
    private List<string> itemList;
    private InventoryData inventory;

    private Dictionary<string, CurrencyData> cachedCurrencyData = new Dictionary<string, CurrencyData>();

    private void Awake()
    {
        itemList = new List<string>();
        currencyList = new Dictionary<string, int>();

        accessAnimation = GetComponentInChildren<AccessoryAnimation>();
    }
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

        OnCurrencyChanges?.Invoke(currencyData, GetCurrencyAmount(currencyId));
    }
    public void RemoveCurrency(string currencyId, int amount)
    {
        var current = GetCurrencyAmount(currencyId);

        current -= amount;

        SetCurrency(currencyId, current);

        var currencyData = GetCurrencyData(currencyId);

        OnCurrencyChanges?.Invoke(currencyData, GetCurrencyAmount(currencyId));
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
            cachedCurrencyData[currencyId] = CurrencyManager.Instance.GetElementData(currencyId);

        return cachedCurrencyData[currencyId];
    }

    public void EquipItem(ShopItem item)
    {
        switch (item.GetType())
        {
            case ShopController.ItemType.Accessory:
                accessAnimation.EquipItem(item);
                break;
        }
    }
}
