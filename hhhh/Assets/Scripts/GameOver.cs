using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void LoadMenuScene()
    {
        // Load the "Menu" scene
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    private void Start()
    {
        // Unlock the mouse cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
