using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopController : Singleton<ShopController>
{
    public enum ItemType
    {
        Accessory
    }

    public delegate bool CanPurchaseItem(ShopItem shopItem);
    public delegate bool CouldNotPurchaseItem ();

    [SerializeField] ShopView shopView;

    public UnityEvent OnCloseShop;

    public void OpenShop(ShopCollection shopCollection)
    {
        shopView.Setup(shopCollection, CanPurchase, CouldNotPurchase);

        shopView.OnClosed.AddListener(OnClosedShop);
    }

    bool CanPurchase(ShopItem shopItem)
    {
        return true;
    }

    bool CouldNotPurchase()
    {
        return true;
    }

    void OnClosedShop()
    {
        OnCloseShop?.Invoke();
    }
}
