using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicDialogue : MonoBehaviour
{
    public Enums.DialogueTypingType typingType;

    [SerializeField] private List<DialogueEntry> _dialogues = new List<DialogueEntry>();

    private DialogueController _dialogueController;

    public bool isFinal = false;

    private void Awake()
    {
        _dialogueController = FindFirstObjectByType<DialogueController>();
        _dialogueController.DialogueEnded += OnDialogueEnded;
    }

    private void OnDialogueEnded()
    {
        if(isFinal)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    private void Start()
    {
        ShowDialogue();
    }


    private void ShowDialogue()
    {
        if (_dialogueController)
        {
            _dialogueController.StartDialogue(_dialogues, typingType);
        }
    }

    private void OnDestroy()
    {
        _dialogueController.DialogueEnded -= OnDialogueEnded;
    }
}
