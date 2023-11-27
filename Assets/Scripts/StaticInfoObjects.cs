using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// class for storing all CONST fields such as static dictionaries, animation curves, etc
///
/// Also provides useful access functions to them
/// </summary>


public class StaticInfoObjects : MonoBehaviour
{
    public static StaticInfoObjects Instance { get; private set; }

    [SerializeField] public AnimationCurve FADE_ANIM_CURVE;
    [SerializeField] public AnimationCurve OPEN_REALM_CURVE;
    [SerializeField] public AnimationCurve CLOSE_REALM_CURVE;
    [SerializeField] public AnimationCurve CA_OPEN_REALM_CURVE; // chromatic aberration curve
    [SerializeField] public AnimationCurve LD_OPEN_REALM_CURVE; // lens distortion curve
    
    [SerializeField] public AnimationCurve CA_QUICK_IMPULSE; // chromatic aberration curve
    [SerializeField] public AnimationCurve LD_QUICK_IMPULSE; // lens distortion curve
    
    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}