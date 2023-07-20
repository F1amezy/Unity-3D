using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Footsteps : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Ensure you've assigned an AudioSource component to this script in the Inspector
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is not assigned to the CharacterMovementAudio script.");
        }
    }

    void Update()
    {
        // Play audio while moving
        bool isMoving = characterController.isGrounded && characterController.velocity.magnitude > 0.1f;
        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}