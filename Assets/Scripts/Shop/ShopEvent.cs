using MEET_AND_TALK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Shop")]
public class ShopEvent : Event
{
    [SerializeField] ShopCollection shopCollection;

    public override void Trigger(Action<int> OnFinish)
    {
        base.Trigger(OnFinish);

        if(shopCollection == null)
        {
            Debug.LogWarning("Shop Collection not valid. Skipping.");
            OnFinishEvent();
            return;
        }

        ShopController.Instance.OpenShop(shopCollection, playerController);
        ShopController.Instance.OnCloseShop.AddListener(OnFinishEvent);
    }

    void OnFinishEvent()
    {
        onFinish?.Invoke(0);

        ShopController.Instance.OnCloseShop.RemoveListener(OnFinishEvent);
    }

}
