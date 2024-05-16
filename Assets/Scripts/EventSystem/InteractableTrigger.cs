using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableTrigger : Trigger
{
    new private Collider2D collider;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }
}
