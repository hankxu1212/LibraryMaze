using System;
using UnityEngine;

public class LibraryController : MonoBehaviour
{
    private void Start()
    {
        ScriptableRendererFeatureManager.Instance.ToggleAll(false);
    }
}