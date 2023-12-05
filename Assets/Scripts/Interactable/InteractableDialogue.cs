using UnityEngine;
using Ink.Runtime;

public class InteractableDialogue : InteractableObject
{
    [SerializeField] private TextAsset[] inkJSONAssets;
    
    public override void Interact()
    {
        var story = new Story(inkJSONAssets[interactionsOccured].text);
        DialogueUI.Instance.StartDialogue(story);
        interactionsOccured++;
    }
}