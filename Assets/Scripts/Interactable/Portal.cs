using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Valley");
            // SceneManager.LoadScene("GoodEnd", LoadSceneMode.Additive);
        }
    }
}