using System;
using UnityEngine;

public abstract class Event : ScriptableObject
{
    protected PlayerController playerController;
    public virtual void Setup(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public abstract void Trigger(Action OnFinish);
}
