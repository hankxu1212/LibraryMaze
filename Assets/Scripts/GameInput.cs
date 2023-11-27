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
    private void OnDisable()
    {
        gameInputActions.Dispose();
        gameInputActions.Player.Interact.performed -= InteractOnperformed;
    }

    private void InteractOnperformed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

}