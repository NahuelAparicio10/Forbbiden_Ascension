using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public bool isBurstDialogue;
    public Enums.DialogueTypingType typingType;

    [SerializeField] private List<DialogueEntry> _dialogues = new List<DialogueEntry>();

    private DialogueController _dialogueController;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _dialogueController = FindFirstObjectByType<DialogueController>();
        _dialogueController.DialogueEnded += OnDialogueEnded;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerInteractable"))
        {
            ShowDialogue();
        }
    }


    private void ShowDialogue()
    {
        if (_dialogueController)
        {
            _dialogueController.StartDialogue(_dialogues, typingType);
            OnDisableCollider();
        }

        if(isBurstDialogue)
        {
            Destroy(gameObject);
        }
    }

    private void OnDialogueEnded()
    {
        Invoke(nameof(OnEnableCollider), 2f);
    }

    public void OnEnableCollider() => _collider.enabled = true;
    public void OnDisableCollider() => _collider.enabled = false;


    private void OnDestroy()
    {
        _dialogueController.DialogueEnded -= OnDialogueEnded;
    }
}
