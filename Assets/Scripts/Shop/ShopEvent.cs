using MEET_AND_TALK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Shop")]
public class ShopEvent : Event
{
    [SerializeField] ShopCollection shopCollection;

    private Action onFinishEvent;

    public override void Trigger(Action OnFinish)
    {
        base.Trigger(OnFinish);

        ShopController.Instance.OpenShop(shopCollection);
        ShopController.Instance.OnCloseShop.AddListener(OnFinishEvent);
    }

    void OnFinishEvent()
    {
        onFinishEvent?.Invoke();

        ShopController.Instance.OnCloseShop.RemoveListener(OnFinishEvent);
    }

}
