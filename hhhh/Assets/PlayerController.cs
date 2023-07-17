using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Reference to the Animator component of the player
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        // Link the "vertical" parameter in the animator to the player's vertical input
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        // Link the "horizontal" parameter in the animator to the player's horizontal input
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
    }
}