using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTrigger : MonoBehaviour
{
    public string endSceneName = "EndScene"; // The name of the end scene to load

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player (you can also check by tag if you've tagged your player)
        if (other.CompareTag("Player"))
        {
            // Load the end scene
            SceneManager.LoadScene(endSceneName);
        }
    }
}