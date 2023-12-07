using System;
using System.Collections;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;

public class Ending : InteractableObject
{
    public static Ending Instance;


    [SerializeField] private TextAsset[] inkJSONAssets;
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private GameObject doorToOpen;
    [SerializeField] [TextArea] private string answer;
    [SerializeField] [TextArea] private string puzzleText;

    public int ending = -1; // 1 for good, 0 for bad
    
    private void Awake()
    {
        Instance = this;
    }
    
    public override void Interact()
    {
        if (interactionsOccured == 0)
        {
            var story = new Story(inkJSONAssets[0].text);
            DialogueUI.Instance.StartDialogue(story);
            interactionsOccured = 1;
            puzzle.ResetPuzzle();
        }
        else if (interactionsOccured == 1)
        {
            int e = DialogueUI.Instance.choicesMadeLast[4].index;
            if (e == 1) // good ending
            {
                puzzle.ActivateCongratsAndSetText("You have triggered ending 1. Solve the puzzle and enter the door.");
                ending = 1;
                var story = new Story(inkJSONAssets[1].text);
                DialogueUI.Instance.StartDialogue(story);
            }
            if (e == 0) // bad ending
            {
                puzzle.ActivateCongratsAndSetText("You walked away from the puzzle.");
                ending = 0;
                var story = new Story(inkJSONAssets[2].text);
                DialogueUI.Instance.StartDialogue(story);
                StartCoroutine(LoadBadEndingAfterDelay());
            }
            interactionsOccured = 2;
        }
        else if (!puzzle.IsFinished())
        {
            puzzle.BeginPuzzle(doorToOpen, answer, puzzleText);
            interactionsOccured = 3;
        }
    }

    private IEnumerator LoadBadEndingAfterDelay()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("BadEnd");
    }
}