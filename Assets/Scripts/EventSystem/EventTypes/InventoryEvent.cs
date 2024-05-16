using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Inventory")]
public class InventoryEvent : Event
{
    public override void Trigger(Action OnFinish)
    {
        base.Trigger(OnFinish);

        playerController.OpenInventory();
        playerController.OnInventoryClosed.AddListener(OnFinishDialogue);
    }

    void OnFinishDialogue()
    {
        playerController.OnInventoryClosed.RemoveListener(OnFinishDialogue);

        onFinish?.Invoke();
    }
}
