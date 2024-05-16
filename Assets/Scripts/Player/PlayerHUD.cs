using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ShopController;

public class PlayerHUD : MonoBehaviour
{
    public delegate ShopItem GetItemData(string itemId);

    [SerializeField] CurrencyContainerView currencyContainer;
    [SerializeField] InventoryView itemListView;

    private GetCurrencyAmount onGetCurrencyAmount;

    public UnityEvent OnClosedInventory;

    public void Setup(List<CurrencyData> displayCurrencies, GetCurrencyAmount onGetCurrencyAmount)
    {
        currencyContainer.Setup(displayCurrencies, onGetCurrencyAmount);
    }

    public void OpenInventory(PlayerInventory playerInventory, GetItemData getItemData)
    {
        itemListView.Setup(playerInventory.GetItemList(), getItemData);
        itemListView.OnClosed.AddListener(OnCloseInventory);
    }
    public void OnCloseInventory()
    {
        OnClosedInventory?.Invoke();
    }


    public void UpdateCurrencyValue(CurrencyData currency, int newAmount)
    {
        currencyContainer.UpdateCurrencyValue(currency, newAmount);
    }
}
