using MEET_AND_TALK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/Shop")]
public class ShopEvent : Event
{
    [SerializeField] ShopCollection shopCollection;

    private Action onFinishEvent;

    public override void Trigger(Action OnFinish)
    {
        
    }


}
