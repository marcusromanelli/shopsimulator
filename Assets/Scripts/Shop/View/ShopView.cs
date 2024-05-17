using MEET_AND_TALK;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static PlayerInventory;
using static ShopController;

[RequireComponent(typeof(CanvasGroup))]
public class ShopView : MonoBehaviour
{
    [SerializeField] ShopNPCContainerView npcContainerView;
    [SerializeField] ShopViewItemContainer itemContainerView;
    [SerializeField] CurrencyContainerView currencyContainer;

    public UnityEvent<ShopItem> OnPurchased;
    public UnityEvent OnClosed;

    private ShopCollection shopCollection;
    private CanvasGroup canvasGroup;
    private CanPurchaseItem canPurchaseItem;
    private CouldNotPurchaseItem couldNotPurchaseItem;
    private GetCurrencyAmount getCurrencyAmount;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Setup(ShopCollection shopCollection, CanPurchaseItem canPurchaseItem, CouldNotPurchaseItem couldNotPurchaseItem, GetCurrencyAmount getCurrencyAmount)
    {
        this.shopCollection = shopCollection;
        this.canPurchaseItem = canPurchaseItem;
        this.couldNotPurchaseItem = couldNotPurchaseItem;
        this.getCurrencyAmount = getCurrencyAmount;

        Initialize();
    }
    public void Close()
    {
        OnClosed?.Invoke();
        itemContainerView.Close();
        currencyContainer.Close();
        CloseWindow();
    }

    public void UpdateCurrencyValue(CurrencyData currencyData, int amount)
    {
        currencyContainer.UpdateCurrencyValue(currencyData, amount);
    }
    void Initialize()
    {
        npcContainerView.Setup(shopCollection.GetSeller());
        itemContainerView.Setup(shopCollection.GetItems(), canPurchaseItem, couldNotPurchaseItem);
        itemContainerView.onPurchase.AddListener(OnItemPurchased);

        currencyContainer.Setup(shopCollection.GetAcceptedCurrencies(), getCurrencyAmount);

        OpenWindow();
    }

    void CloseWindow()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        itemContainerView.onPurchase.RemoveAllListeners();
    }

    void OpenWindow()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void OnItemPurchased(ShopItem shopItem)
    {
        OnPurchased?.Invoke(shopItem);
    }

}