using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class ShopViewItemContainer : MonoBehaviour
{
    [SerializeField] ShopViewObject ShopItemPrefab;
    [SerializeField] Transform ItemContainer;

    public UnityEvent<ShopItem> onPurchase;

    private ShopItem[] itemArray;
    private GenericPool<ShopViewObject> shopItemPool;
    private List<ShopViewObject> itemObjects;

    private void Awake()
    {
        shopItemPool = new GenericPool<ShopViewObject>();
        itemObjects = new List<ShopViewObject>();
    }
    public void Setup(ShopItem[] itemArray)
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
            itemObj.OnClickPurchase.AddListener(OnPurchased);

            itemObjects.Add(itemObj);
        }
    }

    void OnPurchased(ShopItem item)
    {
        onPurchase?.Invoke(item);
    }
}
