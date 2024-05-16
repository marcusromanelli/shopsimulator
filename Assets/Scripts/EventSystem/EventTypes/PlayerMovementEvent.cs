using System;
using UnityEngine;

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
