using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    [SerializeField] internal GameObject _dialogueHolder;

    private TypingHandler _typeHandler;
    private List<DialogueEntry> _currentDialogue = new List<DialogueEntry>();
    private int _indexDialogue = 0;
    private PlayerInputsSystem _actions;


    public event Action DialogueStarted;
    public event Action DialogueEnded;

    private void Awake()
    {
        _typeHandler = GetComponent<TypingHandler>();
        EnableInputs();
        _dialogueHolder.SetActive(false);
    }

    public void StartDialogue(List<DialogueEntry> messages, Enums.DialogueTypingType t)
    {
        if (messages == null || messages.Count == 0) return;

        _dialogueHolder.SetActive(true);
        _typeHandler.SetTypingType(t);

        _currentDialogue.Clear();
        _currentDialogue.AddRange(messages);

        _typeHandler.ShowMessage(_currentDialogue[_indexDialogue].text);
        _currentDialogue[_indexDialogue].Invoke();
        _indexDialogue++;

        DialogueStarted?.Invoke();

        _actions.Player.Interact.performed += NextDialogue_performed;
    }

    private void HideDialogueBox()
    {
        Invoke(nameof(EndDialogue), 0.25f);
    }
    private void EndDialogue()
    {
        DialogueEnded?.Invoke();
        _currentDialogue.Clear();
        _typeHandler.ResetTypingType();
        _typeHandler.ResetText();
        _indexDialogue = 0;
        _dialogueHolder.SetActive(false);
        _actions.Player.Interact.performed -= NextDialogue_performed;
    }

    private void EnableInputs()
    {
        _actions = new PlayerInputsSystem();
        _actions.Player.Enable();

    }

    private float _nextInteractTime = 0f;
    private float _interactCooldown = 0.25f; // Cooldown time
    private void NextDialogue_performed(InputAction.CallbackContext context)
    {
        if (_currentDialogue.Count <= 0 || Time.time < _nextInteractTime) return;

        if (!_typeHandler.IsDialoguePrinted)
        {
            _typeHandler.FinishTypingImmediately();
        }
        else
        {
            if (_indexDialogue < _currentDialogue.Count)
            {
                _typeHandler.ShowMessage(_currentDialogue[_indexDialogue].text);
                _currentDialogue[_indexDialogue].Invoke();
                _indexDialogue++;
            }
            else
            {
                HideDialogueBox();
            }
        }

        _nextInteractTime = Time.time + _interactCooldown;
    }
}
