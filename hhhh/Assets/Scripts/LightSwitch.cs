using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour
{
    public bool onSwitch;
    public bool lightStatus;
    public GameObject theLight;
    void OnTriggerEnter(Collider player)
    {
        onSwitch = true;
    }
    void OnTriggerExit(Collider player)
    {
        onSwitch = false;
    }
    void Update()
    {
        if (theLight.active == true)
        {
            lightStatus = true;
        }
        else
        {
            lightStatus = false;
        }
        if (onSwitch)
        {
            if (lightStatus)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    theLight.active = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    theLight.active = true;
                }
            }
        }
    }
    
}
