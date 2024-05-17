using MEET_AND_TALK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Lock Input")]
public class LockInputEvent : Event
{
    [SerializeField] bool inputEnabled;

    private Action onFinishEvent;

    public override void Trigger(Action<int> OnFinish)
    {
        base.Trigger(OnFinish);

        if (inputEnabled)
            playerController.EnableInput();
        else
            playerController.DisableInput();

        onFinish?.Invoke(-1);
    }
}
