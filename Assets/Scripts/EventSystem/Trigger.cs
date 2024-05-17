using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] EventCollection events;
    [SerializeField] UnityEvent<int> OnFinish;

    private int currentEventIndex = 0;
    private bool isTriggering;
    private PlayerController playerController;

    public void TryTriggerEvents(PlayerController playerController)
    {
        if (isTriggering)
            return;

        isTriggering = true;

        currentEventIndex = -1;
        this.playerController = playerController;
;
        RunTriggerList(-1);
    }

    void RunTriggerList(int lastEventReturnedValue)
    {
        currentEventIndex++;

        var nextEvent = GetEvent(currentEventIndex);
        
        if(nextEvent == null)
        {
            isTriggering = false;
            OnFinish?.Invoke(-1);
            return;
        }

        nextEvent.Setup(lastEventReturnedValue, playerController);
        nextEvent.Trigger(RunTriggerList);
    }
    Event GetEvent(int eventIndex)
    {
        return events.GetEvent(eventIndex);
    }
}
