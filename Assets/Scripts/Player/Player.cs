using System;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

/// <summary>
/// The main partial class for Player
/// </summary>

[RequireComponent(typeof(CharacterController))]
public partial class Player : MonoBehaviour
{
    private PlayerInput _playerInput;
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        GameInput.Instance.OnInteract += OnInteract;
        GameEventsManager.Instance.GameStateEvents.OnPauseToggle += GameStateEventsOnOnPauseToggle;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnInteract -= OnInteract;
        GameEventsManager.Instance.GameStateEvents.OnPauseToggle -= GameStateEventsOnOnPauseToggle;
    }

    private void GameStateEventsOnOnPauseToggle(bool isToggled)
    {
        if (isToggled)
            _playerInput.DeactivateInput();
        else
            _playerInput.ActivateInput();
    }

    private void OnInteract()
    {
        Interact();
    }
}
