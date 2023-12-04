using System;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

/// <summary>
/// The main partial class for Player
/// </summary>

[RequireComponent(typeof(CharacterController))]
public partial class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnInteract += OnInteract;
    }

    private void OnInteract()
    {
        Interact();
    }
}
