﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// THE UI baseclass. Provides useful functions for transitioning.
/// <remarks>Only use this on parent MonoBehaviours that needs to fade in/out!
/// Otherwise it will come with unnecessary overhead</remarks>
/// 
/// Inheritors have access to
/// <code>Show()</code>
/// Calls Activate() on the object, enables it, (or the container object if alternativeGameObject is toggled on),
/// and begins the Fade transition.
/// 
/// <code>Hide()</code>
/// Calls Deactivate() on the object, disables it, and begins the Fade out transition
/// <code>Init()</code>
/// Obtains the canvasGroup and rectTransform components from the gameObject.
/// 
/// NOTE: Inheritors should selectively implement the following event methods:
/// <code>Activate</code> Called before Show()'s fade transitions
/// <code>Deactivate</code> Called before Hide()'s fade transitions
/// <code>LateActivate</code> Called after Show()'s fade transitions
/// <code>LateDeactivate</code> Called after Hide()'s fade transitions
/// 
/// </summary>

[RequireComponent(typeof(CanvasGroup))]
public class UI : MonoBehaviour
{
    [SerializeField] protected GameObject containerGameObject;
    protected CanvasGroup canvasGroup;
    protected RectTransform rectTransform;
    protected bool alternativeGameObject = false;
    private float animationTime = .5f;
    
    protected virtual void Activate() { }
    protected virtual void Deactivate() { }
    protected virtual void LateActivate() { }
    protected virtual void LateDeactivate() { }

    public virtual void Show()
    {
        Activate();
        Display(true);
        StartCoroutine(Fade(0, 1));
    }

    public virtual void Hide()
    {
        Deactivate();
        if(gameObject.activeSelf)
            StartCoroutine(Fade(1, 0));
    }

    protected IEnumerator Fade(float start, float end)
    {
        float time = 0;
        while (time < animationTime)
        {
            time += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(
                start, 
                end, 
                StaticInfoObjects.Instance.FADE_ANIM_CURVE.Evaluate(time * 2)
            );
            yield return null;
        }
        
        canvasGroup.alpha = end;
        
        if (end == 0)
        {
            Display(false);
            LateDeactivate();
        }
        else
        {
            LateActivate();
        }
    }

    private void Display(bool active)
    {
        if (alternativeGameObject)
            containerGameObject.SetActive(active);
        else
            gameObject.SetActive(active);
    }

    protected void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
}