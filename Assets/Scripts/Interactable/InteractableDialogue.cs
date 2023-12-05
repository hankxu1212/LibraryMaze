using UnityEngine;
using Ink.Runtime;

public class InteractableDialogue : InteractableObject
{
    [SerializeField] private TextAsset[] inkJSONAssets;
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private GameObject doorToOpen;
    [SerializeField] [TextArea] private string answer;
    [SerializeField] [TextArea] private string puzzleText;
    
    public override void Interact()
    {
        if (interactionsOccured == 0)
        {
            var story = new Story(inkJSONAssets[0].text);
            DialogueUI.Instance.StartDialogue(story);
            interactionsOccured = 1;
            puzzle.ResetPuzzle();
        }
        else if (puzzle.IsFinished())
        {
            var story = new Story(inkJSONAssets[1].text);
            DialogueUI.Instance.StartDialogue(story);
            interactionsOccured = 2;
        }
        else
        {
            puzzle.BeginPuzzle(doorToOpen, answer, puzzleText);
        }
    }
}