using UnityEngine;

public class KeepOnLoad : MonoBehaviour
{
    public static KeepOnLoad Instance;
    
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