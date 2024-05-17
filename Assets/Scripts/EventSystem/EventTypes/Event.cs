using System;
using UnityEngine;

public abstract class Event : ScriptableObject
{
    protected Action<int> onFinish;
    protected PlayerController playerController;
    protected int lastEventReturnedValue;
    public virtual void Setup(int lastEventReturnedValue, PlayerController playerController)
    {
        this.playerController = playerController;
        this.lastEventReturnedValue = lastEventReturnedValue;
    }
    public virtual void Trigger(Action<int> OnFinish)
    {
        this.onFinish = OnFinish;
    }
}
