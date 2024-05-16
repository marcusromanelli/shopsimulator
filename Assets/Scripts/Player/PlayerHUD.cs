using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShopController;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] CurrencyContainerView currencyContainer;

    private GetCurrencyAmount onGetCurrencyAmount;

    public void Setup(List<CurrencyData> displayCurrencies, GetCurrencyAmount onGetCurrencyAmount)
    {
        currencyContainer.Setup(displayCurrencies, onGetCurrencyAmount);
    }
    
    public void UpdateCurrencyValue(CurrencyData currency, int newAmount)
    {
        currencyContainer.UpdateCurrencyValue(currency, newAmount);
    }
}
