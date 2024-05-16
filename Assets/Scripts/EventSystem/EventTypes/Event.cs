using System;
using UnityEngine;

public abstract class Event : ScriptableObject
{
    protected Action onFinish;
    protected PlayerController playerController;
    public virtual void Setup(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public virtual void Trigger(Action OnFinish)
    {
        this.onFinish = OnFinish;
    }
}
