using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EventTrigger : Trigger
{
    [SerializeField] bool triggerOnce;

    new private Collider2D collider;
    private bool hasTriggered;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerOnce && hasTriggered)
            return;

        var playerController = collision.GetComponent<PlayerController>();

        TryTriggerEvents(playerController);

        hasTriggered = true;
    }
}
