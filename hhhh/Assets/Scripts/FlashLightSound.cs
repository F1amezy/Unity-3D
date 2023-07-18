using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FlashLightSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            source.PlayOneShot(clip);
        }
    }
}