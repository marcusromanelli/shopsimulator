using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class EventTrigger : Trigger
{
    new private Collider2D collider;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerController = collision.GetComponent<PlayerController>();

        TryTriggerEvents(playerController);
    }
}
