using UnityEngine;

public class PortalButton : MonoBehaviour
{
    public GameObject firstObject; // The first selectable game object
    public GameObject secondObject; // The second selectable game object
    public Transform player; // Reference to the player's transform
    public float interactRange = 5f; // The range within which the objects can be interacted with

    private bool hasInteracted = false; // To track if the interaction has already happened

    void Update()
    {
        // Calculate the distance between the player and the first game object
        float distanceToFirstObject = Vector3.Distance(firstObject.transform.position, player.position);

        // Check if the player is within the specified range and presses the "E" key
        if (distanceToFirstObject <= interactRange && Input.GetKeyDown(KeyCode.E) && !hasInteracted)
        {
            // Enable the second game object
            secondObject.SetActive(true);

            // Mark the interaction as completed
            hasInteracted = true;
        }
    }
}
