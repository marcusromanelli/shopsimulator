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

    public void Setup(List<CurrencyData> acceptedCurrencies, GetCurrencyAmount onGetCurrencyAmount)
    {
        this.onGetCurrencyAmount = onGetCurrencyAmount;
        Initialize(acceptedCurrencies, onGetCurrencyAmount);
    }
    public void UpdateCurrencyValue(CurrencyData currencyData, int amount)
    {
        if (!GetCurrentCurrencies().Contains(currencyData.GetId()))
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

    public void Close()
    {
        foreach (var currency in currencyObjects)
            GetPool().Release(currency);

        currencyObjects.Clear();
    }

    private void InstantiateCurrencyObject(CurrencyData currency, GetCurrencyAmount onGetCurrencyAmount)
    {
        InstantiateCurrencyObject(currency, onGetCurrencyAmount(currency.GetId()));
    }

    private void InstantiateCurrencyObject(CurrencyData currency, int startAmount)
    {
        if (GetCurrentCurrencies().Contains(currency.GetId()))
            return;

        var obj = GetPool().Get();

        obj.Setup(currency, startAmount);
        obj.transform.SetParent(currencyContainer);
        obj.transform.localScale = Vector3.one;
        currencyObjects.Add(obj);

        GetCurrentCurrencies().Add(currency.GetId());
    }

    GenericPool<CurrencyDataObject> GetPool()
    {
        if (currencyPool == null)
            currencyPool = new GenericPool<CurrencyDataObject>(currencyPrefab);

        return currencyPool;
    }

    List<string> GetCurrentCurrencies()
    {
        if (currentCurrencies == null)
            currentCurrencies  = new List<string> ();

        return currentCurrencies;
    }
}
