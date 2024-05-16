using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ShopController;

public class PlayerHUD : MonoBehaviour
{
    public delegate ShopItem GetItemData(string itemId);

    [SerializeField] CurrencyContainerView currencyContainer;
    [SerializeField] InventoryView inventoryView;

    private GetCurrencyAmount onGetCurrencyAmount;

    public UnityEvent OnClosedInventory;
    public UnityEvent<ShopItem> OnClickedItem;

    public void Setup(List<CurrencyData> displayCurrencies, GetCurrencyAmount onGetCurrencyAmount)
    {
        currencyContainer.Setup(displayCurrencies, onGetCurrencyAmount);
    }

    public void OpenInventory(PlayerInventory playerInventory, GetItemData getItemData)
    {
        inventoryView.Setup(playerInventory.GetItemList(), getItemData);
        inventoryView.OnClosed.AddListener(OnCloseInventory);
        inventoryView.OnClickedItem.AddListener(OnClickItem);
    }
    public void OnCloseInventory()
    {
        OnClosedInventory?.Invoke();
    }
    public void OnClickItem(ShopItem shopItem)
    {
        OnClickedItem?.Invoke(shopItem);
    }

    public void UpdateCurrencyValue(CurrencyData currency, int newAmount)
    {
        currencyContainer.UpdateCurrencyValue(currency, newAmount);
    }
}
