using System;
using UnityEngine;
using TMPro;

public class InteractUI : UI
{
    [SerializeField] private TextMeshProUGUI interactText;
    private IInteractable interactable;
    private IInteractable lastInteractable;

    private bool isShowing; 

    private void Awake()
    {
        alternativeGameObject = true;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {    
        Hide();
    }
    
    private void ToggleActive(bool paused)
    {
        gameObject.SetActive(!paused);
    }

    private void Update()
    {
        lastInteractable = interactable;
        interactable = Player.Instance.GetInteractableObject();

        if (interactable != null && (!isShowing || interactable != lastInteractable))
        {
            StopAllCoroutines();
            Show();
        }
        else if(interactable == null && isShowing)
        {
            Hide();
        }
    }
    
    protected override void Activate()
    {
        interactText.text = interactable.GetInteractText();
        isShowing = true;
    }
    
    protected override void Deactivate()
    {
        isShowing = false;
    }
}
