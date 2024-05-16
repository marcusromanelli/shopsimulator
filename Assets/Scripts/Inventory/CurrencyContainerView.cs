using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static ShopController;

public class CurrencyContainerView : MonoBehaviour
{
    [SerializeField] CurrencyDataObject currencyPrefab;
    [SerializeField] Transform currencyContainer;
    

    
    private GetCurrencyAmount onGetCurrencyAmount;
    private GenericPool<CurrencyDataObject> currencyPool;
    private List<CurrencyDataObject> currencyObjects;

    private List<string> currentCurrencies;

    private void Awake()
    {
        currencyPool = new GenericPool<CurrencyDataObject>(currencyPrefab);
        currentCurrencies = new List<string>();
    }
    public void Setup(List<CurrencyData> acceptedCurrencies, GetCurrencyAmount onGetCurrencyAmount)
    {
        this.onGetCurrencyAmount = onGetCurrencyAmount;
        Initialize(acceptedCurrencies, onGetCurrencyAmount);
    }
    public void UpdateCurrencyValue(CurrencyData currencyData, int amount) {

        if (!currentCurrencies.Contains(currencyData.GetId()))
        {
            InstantiateCurrencyObject(currencyData, amount);
            return;
        }

        foreach (var currency in currencyObjects)
        {
            currency.UpdateCurrencyValue(currencyData, amount);
        }
    }


    private void Initialize(List<CurrencyData> acceptedCurrencies, GetCurrencyAmount onGetCurrencyAmount)
    {
        currencyObjects = new List<CurrencyDataObject>();

        foreach(var currency in acceptedCurrencies)
        {
            InstantiateCurrencyObject(currency, onGetCurrencyAmount);
        }
    }

    private void OnDisable()
    {
        if(currencyObjects != null)
            foreach (var currencyObject in currencyObjects)
                currencyPool.Release(currencyObject);
    }

    private void InstantiateCurrencyObject(CurrencyData currency, GetCurrencyAmount onGetCurrencyAmount)
    {
        InstantiateCurrencyObject(currency, onGetCurrencyAmount(currency.GetId()));
    }

    private void InstantiateCurrencyObject(CurrencyData currency, int startAmount)
    {
        if (currentCurrencies.Contains(currency.GetId()))
            return;

        var obj = currencyPool.Get();

        obj.Setup(currency, startAmount);
        obj.transform.SetParent(currencyContainer);
        obj.transform.localScale = Vector3.one;
        currencyObjects.Add(obj);

        currentCurrencies.Add(currency.GetId());
    }
}
