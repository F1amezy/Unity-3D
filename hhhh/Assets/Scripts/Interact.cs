using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject targetObject; // The game object to toggle
    public Transform player; // Reference to the player's transform
    public float range = 5f; // The range within which the object can be toggled

    private bool isObjectActive = true;

    void Start()
    {
        // Ensure that the target object starts in the desired active state
        targetObject.SetActive(isObjectActive);
    }

    void Update()
    {
        // Calculate the distance between the player and the target object
        float distanceToTarget = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the specified range and presses the "E" key
        if (distanceToTarget <= range && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the state of the target object
            isObjectActive = !isObjectActive;
            targetObject.SetActive(isObjectActive);
        }
    }
}
