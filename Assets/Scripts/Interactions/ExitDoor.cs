using MEET_AND_TALK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] DialogueContainerSO dialogue;

    PlayerController player;
    void Start()
    {
        dialogueManager.EndDialogueEvent.AddListener(OnDialogueEnded);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entrou!");
        player = collision.gameObject.GetComponent<PlayerController>();

        StartDialogue();
    }

    void StartDialogue()
    {
        dialogueManager.StartDialogue(dialogue);

        player.DisableInput();
    }
    void OnDialogueEnded()
    {
        player.EnableInput();
    }
}
