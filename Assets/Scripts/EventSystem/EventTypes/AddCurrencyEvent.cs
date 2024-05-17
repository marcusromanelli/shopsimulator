using MEET_AND_TALK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Currency")]
public class AddCurrencyEvent : Event
{
    [SerializeField] CurrencyData currencyData;
    [SerializeField] int amount;

    public override void Trigger(Action<int> OnFinish)
    {
        base.Trigger(OnFinish);

        playerController.AddCurrency(currencyData, amount);

        onFinish?.Invoke(0);
    }
}
