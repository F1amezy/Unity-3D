using UnityEngine;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour
{
    public float interactionRange = 3f; // The range within which the player can interact with objects
    public Text counterText; // Reference to the UI Text component that displays the counter
    public int maxObjectsToDisable = 10; // The maximum number of game objects the player can disable
    public Toggle allDisabledToggle; // Reference to the UI Toggle component to indicate if all objects are disabled

    private int disabledObjectsCount = 0; // Counter for disabled objects

    void Update()
    {
        // Check for player input (Pressing 'E') and perform interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformInteraction();
        }
    }

    void PerformInteraction()
    {
        // Check for nearby selectable GameObjects within the interaction range
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in nearbyColliders)
        {
            GameObject interactedObject = collider.gameObject;

            // Check if the interacted object is selectable and currently active
            if (interactedObject.activeInHierarchy && interactedObject.CompareTag("Selectable"))
            {
                // Disable the object and increase the counter
                interactedObject.SetActive(false);
                disabledObjectsCount++;

                // Update the counter text on the screen
                UpdateCounterText();
            }
        }

        // Check if all objects are disabled and update the toggle state
        allDisabledToggle.isOn = disabledObjectsCount == maxObjectsToDisable;
    }

    void UpdateCounterText()
    {
        // Display the number of disabled objects and the max limit on the screen
        counterText.text = $"{disabledObjectsCount}/{maxObjectsToDisable}";
    }
}