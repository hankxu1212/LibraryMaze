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
    [Header("Fields")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnInteract += OnInteract;
    }

    private void Update()
    {
    }

    private void OnInteract()
    {
        Interact();
    }
}