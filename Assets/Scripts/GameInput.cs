using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    public event Action OnInteract;

    private GameInputActions gameInputActions;

    private void Awake()
    {
        gameInputActions = new GameInputActions();
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // enable all maps
        gameInputActions.Player.Enable();
        gameInputActions.Player.Interact.performed += InteractOnperformed;
    }

    private void Start()
    {
        GameEventsManager.Instance.GameStateEvents.OnPauseToggle += ToggleUI;
    }

    private void OnDisable()
    {
        gameInputActions.Dispose();
        gameInputActions.Player.Interact.performed -= InteractOnperformed;
        GameEventsManager.Instance.GameStateEvents.OnPauseToggle -= ToggleUI;
    }

    private void InteractOnperformed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }
    
    private void ToggleUI(bool isToggled)
    {
        if (isToggled)
        {
            gameInputActions.Player.Disable();
        }
        else
        {
            gameInputActions.Player.Enable();
        }
    }
}