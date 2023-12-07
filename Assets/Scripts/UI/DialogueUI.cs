using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueUI : UI
{
	public static DialogueUI Instance { get; private set; }
	
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private GameObject choicesHolder;
    [SerializeField] private ButtonUI buttonPrefab;
    
    private Story story;
    private const int MAX_CHOICES = 3;
    private Coroutine displayLineCoroutine;

    public List<Choice> choicesMadeLast;

    private void Awake()
    {
	    Instance = this;
	    canvasGroup = GetComponent<CanvasGroup>();
	    rectTransform = choicesHolder.GetComponent<RectTransform>();
	    gameObject.SetActive(false);
    }

    public void StartDialogue(Story newStory)
    {
	    choicesMadeLast = new();
	    GameEventsManager.Instance.GameStateEvents.PauseToggle(true);
	    Show();
        story = newStory;
        RefreshView();
    }
    
    // This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	private void RefreshView () {
		RemoveChildren();
		
		while (story.canContinue)
		{
			if(displayLineCoroutine != null)
				StopCoroutine(displayLineCoroutine);
			displayLineCoroutine = StartCoroutine(DisplayLine(story.Continue().Trim()));
		}

		// Display all the choices, if there are any! [HARD UPPER-LIMIT: 3 choices]
		if(story.currentChoices.Count > MAX_CHOICES)
			Debug.LogError("Too many choices for this dialogue!");
		
		if(story.currentChoices.Count > 0)
		{
			foreach (Choice choice in story.currentChoices) {
				CreateChoiceView(choice.text.Trim(), () =>
				{
					// Debug.Log(choice.text);
					OnClickChoiceButton(choice);
				});
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(choicesHolder.GetComponent<RectTransform>());
		}
		else
		{
			CreateChoiceView("End conversation", EndDialogue);
		}
	}
	
	private IEnumerator DisplayLine(string line) 
	{
		// set the text to the full line, but set the visible characters to 0
		displayText.text = line;
		displayText.maxVisibleCharacters = 0;

		// display each letter one at a time
		foreach (var _ in line)
		{
			displayText.maxVisibleCharacters++;
			yield return new WaitForSeconds(0.04f);
		}

		displayLineCoroutine = null;
	}

	private void EndDialogue()
	{
		if (displayLineCoroutine != null)
		{
			displayText.maxVisibleCharacters = displayText.text.Length;
			StopCoroutine(displayLineCoroutine);
			displayLineCoroutine = null;
		}
		else
		{
			Hide();
			GameEventsManager.Instance.GameStateEvents.PauseToggle(false);
		}
	}

	// creates a choice view on button index i
	private void CreateChoiceView(string choiceText, UnityAction call)
	{
		ButtonUI choiceButton = Instantiate (buttonPrefab, choicesHolder.transform);
		choiceButton.SetText(choiceText);
		choiceButton.AddListener(call);
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		if (displayLineCoroutine != null)
		{
			displayText.maxVisibleCharacters = displayText.text.Length;
			StopCoroutine(displayLineCoroutine);
			displayLineCoroutine = null;
		}
		else
		{
			choicesMadeLast.Add(choice);
			story.ChooseChoiceIndex (choice.index);
			RefreshView();
		}
	}
	
	void RemoveChildren () {
		int childCount = choicesHolder.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			Destroy (choicesHolder.transform.GetChild (i).gameObject);
		}
	}
}