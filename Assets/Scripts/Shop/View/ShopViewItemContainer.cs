using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using static ShopController;

public class ShopViewItemContainer : MonoBehaviour
{
    [SerializeField] ShopViewObject ShopItemPrefab;
    [SerializeField] Transform ItemContainer;

    public UnityEvent<ShopItem> onPurchase;

    private ShopItem[] itemArray;
    private GenericPool<ShopViewObject> shopItemPool;
    private List<ShopViewObject> itemObjects;
    private CanPurchaseItem canPurchaseItem;
    private CouldNotPurchaseItem couldNotPurchaseItem;

    private void Awake()
    {
        shopItemPool = new GenericPool<ShopViewObject>();
        itemObjects = new List<ShopViewObject>();
    }
    public void Setup(ShopItem[] itemArray, CanPurchaseItem canPurchaseItem, CouldNotPurchaseItem couldNotPurchaseItem)
    {
        this.itemArray = itemArray;

        Initialize();
    }

    void Initialize()
    {
        foreach (var item in itemArray)
        {
            var itemObj = shopItemPool.Get();

            itemObj.Setup(item);
            itemObj.OnClickPurchase.AddListener(TryPurchase);

            itemObjects.Add(itemObj);
        }
    }

    void TryPurchase(ShopItem item)
    {
        if (!canPurchaseItem(item))
        {
            couldNotPurchaseItem();
            return;
        }

        onPurchase?.Invoke(item);
    }
}
