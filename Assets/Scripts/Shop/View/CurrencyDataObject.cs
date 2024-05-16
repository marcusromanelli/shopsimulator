using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static ShopController;

public class CurrencyDataObject : MonoBehaviour, PoolableObject
{
    [SerializeField] Image currencyImage;
    [SerializeField] TMP_Text currencyAmount;   

    
    
    private CurrencyData currency;
    private GetCurrencyAmount getCurrencyAmount;


    public void Setup(CurrencyData currency, GetCurrencyAmount getCurrencyAmount)
    {
        this.currency = currency;
        var startAmount = getCurrencyAmount(currency.GetId());

        UpdateCurrencyValue(currency, startAmount);
    }
    public void Setup(CurrencyData currency, int amount)
    {
        this.currency = currency;
        var startAmount = amount;

        UpdateCurrencyValue(currency, startAmount);
    }

    public void UpdateCurrencyValue(CurrencyData currency, int amount)
    {
        if (currency != this.currency)
            return;

        currencyImage.sprite = currency.GetIcon();
        currencyAmount.text = amount.ToString();
    }
    public void UpdateCurrencyValue(string currencyId, int amount)
    {
        if (currencyId != this.currency.GetId())
            return;

        currencyAmount.text = amount.ToString();
    }

    public void Setup()
    {
        
    }

    public void OnEnabled()
    {
        
    }

    public void OnDisabled()
    {
        currency = null;
    }

    public void Destroy()
    {
        
    }
}