using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopView : MonoBehaviour
{
    [SerializeField] ShopNPCContainerView npcContainerView;
    [SerializeField] ShopViewItemContainer itemContainerView;

    public UnityEvent<ShopItem> OnPurchased;

    private ShopCollection shopCollection;

    public void Setup(ShopCollection shopCollection)
    {
        this.shopCollection = shopCollection;

        Initialize();
    }

    void Initialize()
    {
        npcContainerView.Setup(shopCollection.GetSeller());
        itemContainerView.Setup(shopCollection.GetItems());
    }
}