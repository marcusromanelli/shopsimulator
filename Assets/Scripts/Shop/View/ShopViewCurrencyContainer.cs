using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static ShopController;

public class ShopViewCurrencyContainer : MonoBehaviour
{
    [SerializeField] CurrencyDataObject currencyPrefab;
    [SerializeField] Transform currencyContainer;
    

    
    private GetCurrencyAmount onGetCurrencyAmount;
    private GenericPool<CurrencyDataObject> currencyPool;
    private List<CurrencyDataObject> currencyObjects;

    private void Awake()
    {
        currencyPool = new GenericPool<CurrencyDataObject>(currencyPrefab);
    }
    public void Setup(List<CurrencyData> acceptedCurrencies, GetCurrencyAmount onGetCurrencyAmount)
    {
        Initialize(acceptedCurrencies, onGetCurrencyAmount);
    }
    public void UpdateCurrencyValue(string currencyId, int amount) { 
        foreach (var currency in currencyObjects)
        {
            currency.UpdateCurrencyValue(currencyId, amount);
        }
    }


    private void Initialize(List<CurrencyData> acceptedCurrencies, GetCurrencyAmount onGetCurrencyAmount)
    {
        currencyObjects = new List<CurrencyDataObject>();

        foreach(var currency in acceptedCurrencies)
        {
            var obj = currencyPool.Get();

            obj.Setup(currency, onGetCurrencyAmount);
            obj.transform.SetParent(currencyContainer);
            obj.transform.localScale = Vector3.one;
            currencyObjects.Add(obj);
        }
    }

    private void OnDisable()
    {
        foreach (var currencyObject in currencyObjects)
            currencyPool.Release(currencyObject);
    }
}
