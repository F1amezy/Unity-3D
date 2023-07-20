using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    private const string FullscreenKey = "IsFullscreen";

    private bool isFullscreen;

    private void Start()
    {
        // Load the fullscreen state from PlayerPrefs (0 for false, 1 for true)
        isFullscreen = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;

        // Apply the fullscreen state
        ApplyFullscreen();
    }

    public void Change()
    {
        // Toggle the fullscreen state
        isFullscreen = !isFullscreen;

        // Apply the fullscreen state
        ApplyFullscreen();

        // Save the fullscreen state to PlayerPrefs (0 for false, 1 for true)
        PlayerPrefs.SetInt(FullscreenKey, isFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        print("Changed screen mode");
    }

    private void ApplyFullscreen()
    {
        // Set the fullscreen state
        Screen.fullScreen = isFullscreen;
    }
}
