using MEET_AND_TALK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Dialogue")]
public class DialogueEvent : Event
{
    [SerializeField] DialogueContainerSO dialogue;

    public override void Trigger(Action<int> OnFinish)
    {
        base.Trigger(OnFinish);

        var manager = DialogueManager.Instance;

        manager.EndDialogueEvent.AddListener(OnFinishDialogue);

        DialogueManager.Instance.StartDialogue(dialogue);
    }

    void OnFinishDialogue(int returnValue)
    {
        DialogueManager.Instance.EndDialogueEvent.RemoveListener(OnFinishDialogue);

        onFinish?.Invoke(returnValue);
    }
}
