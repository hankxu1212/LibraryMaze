using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monologue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private ButtonUI button;
    [SerializeField] private string nextScene;
    [SerializeField] private float speed = 0.02f;
    [SerializeField] private float pause = 0.02f;
    
    private void Start()
    {
        button.Disable();
        StartCoroutine(DisplayLine());
    }

    private IEnumerator DisplayLine() 
    {
        // set the text to the full line, but set the visible characters to 0
        string line = displayText.text;
        displayText.maxVisibleCharacters = 0;

        // display each letter one at a time
        foreach (var c in line)
        {
            if (c == '\n')
                yield return new WaitForSeconds(pause);
            
            displayText.maxVisibleCharacters++;
            yield return new WaitForSeconds(speed);
        }
        
        button.Enable();
        button.AddListener(() => SceneManager.LoadScene(nextScene, LoadSceneMode.Single));
    }
}