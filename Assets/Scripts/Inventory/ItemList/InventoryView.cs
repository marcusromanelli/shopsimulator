using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static PlayerHUD;

public class InventoryView : MonoBehaviour
{
    [SerializeField] ItemObject itemObjectPrefab;
    [SerializeField] Transform itemListContainer;
    [SerializeField] CanvasGroup canvasGroup;

    public UnityEvent<ShopItem> OnClickedItem;
    public UnityEvent OnClosed;

    private GetItemData onGetItemData;
    private GenericPool<ItemObject> itemPool;
    private List<ItemObject> itemObjects;

    public void Setup(List<string> storedItems, GetItemData onGetItemData)
    {
        OpenWindow();
        this.onGetItemData = onGetItemData;
        Initialize(storedItems, onGetItemData);
    }

    public void Close()
    {
        CloseWindow();
        OnClosed?.Invoke();

        OnClickedItem.RemoveAllListeners();
        OnClosed.RemoveAllListeners();
    }

    private void Initialize(List<string> acceptedCurrencies, GetItemData onGetItemData)
    {
        itemObjects = new List<ItemObject>();

        foreach (var currency in acceptedCurrencies)
        {
            InstantiateObject(currency, onGetItemData);
        }
    }

    private void OnDisable()
    {
        if (itemObjects != null)
        {
            foreach (var currencyObject in itemObjects)
                GetPool().Release(currencyObject);

            itemObjects.Clear();
        }


    }

    private void InstantiateObject(string itemId, GetItemData onGetItemData)
    {
        var itemData = onGetItemData(itemId);

        InstantiateObject(itemData);
    }

    private void InstantiateObject(ShopItem itemData)
    {
        var obj = GetPool().Get();

        obj.Setup(itemData);
        obj.OnClicked.AddListener(OnClickItem);
        obj.transform.SetParent(itemListContainer);
        obj.transform.localScale = Vector3.one;
        itemObjects.Add(obj);
    }

    GenericPool<ItemObject> GetPool()
    {
        if (itemPool == null)
            itemPool = new GenericPool<ItemObject>(itemObjectPrefab);

        return itemPool;
    }

    void OnClickItem(ShopItem clickedItem)
    {
        OnClickedItem?.Invoke(clickedItem);
    }

    void CloseWindow()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void OpenWindow()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
