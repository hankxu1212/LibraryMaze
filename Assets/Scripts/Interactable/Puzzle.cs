using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private GameObject inputField;
    [SerializeField] private TextMeshProUGUI puzzleText;
    [SerializeField] private TextMeshProUGUI congrats;
    [SerializeField] private GameObject bg;

    private GameObject doorToOpen;
    private string answer;
    
    private void Awake()
    {
        bg.SetActive(false);
        inputField.SetActive(false);
        puzzleText.gameObject.SetActive(false);
        congrats.text = "";
    }
    

    public enum State
    {
        Started,
        Finished
    }

    public bool IsFinished() => state == State.Finished;

    public void ResetPuzzle() => state = State.Started;

    private State state;
    
    public void BeginPuzzle(GameObject door, string ans, string text)
    {
        doorToOpen = door;
        answer = ans;
        puzzleText.text = text;
        inputField.SetActive(true);
        puzzleText.gameObject.SetActive(true);
        bg.SetActive(true);
        state = State.Started;
        GameEventsManager.Instance.GameStateEvents.PauseToggle(true);
    }

    IEnumerator WaitAndDisappear()
    {
        yield return new WaitForSeconds(2f);
        congrats.gameObject.SetActive(false);
    }
    
    public void GrabInputText(string input)
    {
        if (input.ToLower() == answer.ToLower())
        {
            state = State.Finished;
            
            congrats.gameObject.SetActive(true);
            congrats.text = "Congrats! You have solve the puzzle.";
            StartCoroutine(WaitAndDisappear());
            
            puzzleText.gameObject.SetActive(false);
            inputField.SetActive(false);
            doorToOpen.SetActive(false);
            bg.SetActive(false);
            
            GameEventsManager.Instance.GameStateEvents.PauseToggle(false);
        }
    }
}