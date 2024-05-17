using System;
using UnityEngine;

public abstract class Event : ScriptableObject
{
    protected Action<int> onFinish;
    protected PlayerController playerController;
    public virtual void Setup(int lastEventReturnedValue, PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public virtual void Trigger(Action<int> OnFinish)
    {
        this.onFinish = OnFinish;
    }
}
