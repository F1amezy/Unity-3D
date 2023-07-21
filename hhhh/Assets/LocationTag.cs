using UnityEngine;
using UnityEngine.UI;

public class LocationTag : MonoBehaviour
{
    public Text textElement; // Reference to the UI Text component for fading in
    public float fadeInDuration = 1f; // The duration in seconds for the text to fade in
    public float displayDuration = 5f; // The duration in seconds to display the text
    public float fadeOutDuration = 1f; // The duration in seconds for the text to fade out

    private bool isPlayerInRange = false; // Flag to track if the player is in range
    private bool hasFadedIn = false; // Flag to track if the text has already faded in

    private void Start()
    {
        // Disable the text game object on game start
        textElement.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && !hasFadedIn)
        {
            StartCoroutine(FadeInText());
        }
        else if (!isPlayerInRange && hasFadedIn)
        {
            hasFadedIn = false;
            StartCoroutine(FadeOutText());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private System.Collections.IEnumerator FadeInText()
    {
        hasFadedIn = true;
        textElement.gameObject.SetActive(true);
        float currentFadeTime = 0f;

        while (currentFadeTime < fadeInDuration)
        {
            currentFadeTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, currentFadeTime / fadeInDuration);
            textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(displayDuration);

        StartCoroutine(FadeOutText());
    }

    private System.Collections.IEnumerator FadeOutText()
    {
        float currentFadeTime = 0f;

        while (currentFadeTime < fadeOutDuration)
        {
            currentFadeTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, currentFadeTime / fadeOutDuration);
            textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, alpha);
            yield return null;
        }

        textElement.gameObject.SetActive(false);
    }
}
