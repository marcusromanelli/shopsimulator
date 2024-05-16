using JetBrains.Annotations;
using MEET_AND_TALK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static PlayerInventory;

public class ShopController : Singleton<ShopController>
{
    public enum ItemType
    {
        Accessory
    }

    public delegate bool CanPurchaseItem(ShopItem shopItem);
    public delegate void CouldNotPurchaseItem ();
    public delegate int GetCurrencyAmount (string currencyId);

    [SerializeField] ShopView shopView;
    [SerializeField] DialogueContainerSO dialogueObject;

    public UnityEvent OnCloseShop;

    private PlayerController playerController;
    private ShopCollection shopCollection;

    public void OpenShop(ShopCollection shopCollection, PlayerController playerController)
    {
        this.shopCollection = shopCollection;

        this.playerController = playerController;
        this.playerController.OnCurrencyChanged.AddListener(UpdateCurrencyValue);


        shopView.Setup(shopCollection, OnCanPurchase, OnCouldNotPurchase, OnGetCurrencyAmount);

        shopView.OnClosed.AddListener(OnClosedShop);
        shopView.OnPurchased.AddListener(OnPurchasedItem);
    }

    void UpdateCurrencyValue(string currencyId, int amount)
    {
        shopView.UpdateCurrencyValue(currencyId, amount);
    }
    void OnPurchasedItem(ShopItem shopItem)
    {
        Debug.Log("Purchased item!");

        ShowDialogue(shopCollection.GetSeller().GetRandomSalesPhrase());

        playerController.AddItem(shopItem.GetId());
    }

    int OnGetCurrencyAmount(string currencyId)
    {
        return playerController.GetCurrencyAmount(currencyId);
    }

    bool OnCanPurchase(ShopItem shopItem)
    {
        var costData = shopItem.GetCostData();

        return playerController.GetCurrencyAmount(costData) >= costData.amount;
    }

    void OnCouldNotPurchase()
    {
        ShowDialogue(shopCollection.GetSeller().GetRandomNotEnoughtMoneyPhrase());
    }

    void OnClosedShop()
    {
        OnCloseShop?.Invoke();
    }
    void UpdateShopDialogue(string text)
    {
        dialogueObject.DialogueNodeDatas.First().TextType[0].LanguageGenericType = text;
    }
    void ShowDialogue(string text)
    {
        UpdateShopDialogue(text);

        DialogueManager.Instance.StartDialogue(dialogueObject);
    }
}
