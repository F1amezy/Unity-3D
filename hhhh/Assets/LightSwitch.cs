using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour
{
    public bool onSwitch;
    public bool lightStatus;
    public GameObject theLight;
    void OnTriggerEnter(Collider other)
    {
        onSwitch = true;
    }
    void OnTriggerExit(Collider other)
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
    void OnGUI()
    {
        if (onSwitch)
        {
            if (lightStatus)
            {
                GUI.Box(new Rect(0, 0, 200, 20), "Press E to turn off the light");
            }
            else
            {
                GUI.Box(new Rect(0, 0, 200, 20), "Press E to turn on the light");
            }
        }
    }
}
