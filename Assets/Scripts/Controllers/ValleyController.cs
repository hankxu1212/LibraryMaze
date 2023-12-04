using System;
using UnityEngine;

public class ValleyController : MonoBehaviour
{
    private void Start()
    {
        ScriptableRendererFeatureManager.Instance.EnableOnlyOneFog(0);
    }
}