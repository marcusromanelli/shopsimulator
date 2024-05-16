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

    private Action onFinishEvent;

    public override void Trigger(Action OnFinish)
    {
        onFinishEvent = OnFinish;

        var manager = DialogueManager.Instance;

        manager.EndDialogueEvent.AddListener(OnFinishDialogue);

        DialogueManager.Instance.StartDialogue(dialogue);
    }

    void OnFinishDialogue()
    {
        DialogueManager.Instance.EndDialogueEvent.RemoveListener(OnFinishDialogue);

        onFinishEvent?.Invoke();
    }
}