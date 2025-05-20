using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct DialogueEntry
{
    [TextArea] public string text;
    public UnityEvent onDialogueEvent;
    public DialogueEntry(string entry, UnityEvent _onDialogueEvent)
    {
        text = entry; onDialogueEvent = _onDialogueEvent;
    }

    public void Invoke() => onDialogueEvent?.Invoke();
}
