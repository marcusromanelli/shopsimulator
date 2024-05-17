using MEET_AND_TALK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Action By Return Value")]
public class RunActionByReturnValue : Event
{
    [SerializeField] Event[] eventList;

    public override void Trigger(Action<int> OnFinish)
    {
        base.Trigger(OnFinish);

        var chosenEvent = eventList[lastEventReturnedValue];

        chosenEvent.Setup(0, playerController);
        chosenEvent.Trigger(OnFinishEvent);
    }

    void OnFinishEvent(int returnedValue)
    {
        onFinish?.Invoke(returnedValue);
    }
}
