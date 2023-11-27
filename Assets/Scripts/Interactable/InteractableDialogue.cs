using UnityEngine;
using Ink.Runtime;

public class InteractableDialogue : InteractableObject
{
    [SerializeField] private TextAsset inkJSONAsset;
    public override void Interact()
    {
        onInteractIndicator.Play();
        interactionsOccured++;
        var story = new Story(inkJSONAsset.text);
        DialogueUI.Instance.StartDialogue(story);
        FinishInteract();
    }
}