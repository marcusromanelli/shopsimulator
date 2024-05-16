using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ShopController;

[RequireComponent(typeof(CanvasGroup))]
public class ShopView : MonoBehaviour
{
    [SerializeField] ShopNPCContainerView npcContainerView;
    [SerializeField] ShopViewItemContainer itemContainerView;

    public UnityEvent<ShopItem> OnPurchased;
    public UnityEvent OnClosed;

    private ShopCollection shopCollection;
    private CanvasGroup canvasGroup;
    private CanPurchaseItem canPurchaseItem;
    private CouldNotPurchaseItem couldNotPurchaseItem;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Setup(ShopCollection shopCollection, CanPurchaseItem canPurchaseItem, CouldNotPurchaseItem couldNotPurchaseItem)
    {
        this.shopCollection = shopCollection;
        this.canPurchaseItem = canPurchaseItem;
        this.couldNotPurchaseItem = couldNotPurchaseItem;

        Initialize();
    }
    public void Close()
    {
        OnClosed?.Invoke();
        CloseWindow();
    }

    void Initialize()
    {
        npcContainerView.Setup(shopCollection.GetSeller());
        itemContainerView.Setup(shopCollection.GetItems(), canPurchaseItem, couldNotPurchaseItem);

        OpenWindow();
    }

    void CloseWindow()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    void OpenWindow()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }
}