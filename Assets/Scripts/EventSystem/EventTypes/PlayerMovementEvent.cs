using MEET_AND_TALK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Player Movement")]
public class PlayerMovementEvent : Event
{
    [SerializeField] Vector3Int targetOffsetMovement;

    private Action onFinishEvent;

    public override void Trigger(Action OnFinish)
    {
        onFinishEvent = OnFinish;

        playerController.ForceMovement(targetOffsetMovement, OnFinishMovement);
    }

    void OnFinishMovement()
    {
        onFinishEvent?.Invoke();
    }
}
