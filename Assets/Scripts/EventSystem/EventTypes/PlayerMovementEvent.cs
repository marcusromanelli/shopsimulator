using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Player Movement")]
public class PlayerMovementEvent : Event
{
    [SerializeField] Vector3Int targetOffsetMovement;

    private Action onFinishEvent;

    public override void Trigger(Action OnFinish)
    {
        base.Trigger(OnFinish);

        playerController.ForceMovement(targetOffsetMovement, OnFinishMovement);
    }

    void OnFinishMovement()
    {
        onFinish?.Invoke();
    }
}
