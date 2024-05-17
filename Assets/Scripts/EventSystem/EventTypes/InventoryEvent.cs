using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Inventory")]
public class InventoryEvent : Event
{
    public override void Trigger(Action<int> OnFinish)
    {
        base.Trigger(OnFinish);

        playerController.OpenInventory();
        playerController.OnInventoryClosed.AddListener(this.OnFinish);
    }

    void OnFinish()
    {
        playerController.OnInventoryClosed.RemoveListener(OnFinish);

        onFinish?.Invoke(0);
    }
}
