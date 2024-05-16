using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class EventTrigger : MonoBehaviour
{
    [SerializeField] Event[] events;
    [SerializeField] UnityEvent OnFinish;

    new private Collider2D collider;
    private int currentEventIndex = 0;
    private bool isTriggering;
    private PlayerController playerController;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryTriggerEvents(collision.gameObject);
    }

    void TryTriggerEvents(GameObject obj)
    {
        if (isTriggering)
            return;

        currentEventIndex = -1;
        playerController = obj.GetComponent<PlayerController>()
;
        RunTriggerList();
    }

    void RunTriggerList()
    {
        currentEventIndex++;

        var nextEvent = GetEvent(currentEventIndex);
        
        if(nextEvent == null)
        {
            OnFinish?.Invoke();
            return;
        }

        nextEvent.Setup(playerController);
        nextEvent.Trigger(RunTriggerList);
    }
    Event GetEvent(int eventIndex)
    {
        if (eventIndex < 0 || eventIndex >= events.Length)
            return null;

        return events[eventIndex];
    }


}
