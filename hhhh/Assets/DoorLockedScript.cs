using UnityEngine;
using UnityEngine.UI;

public class DoorLockedScript : MonoBehaviour
{
    public Toggle toggle; // Reference to the Toggle component
    public GameObject targetObject; // The GameObject to turn off

    private void Start()
    {
        // Subscribe to the onValueChanged event of the Toggle
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        // Check if the Toggle is checked (isOn is true)
        if (isOn)
        {
            // Disable the target GameObject
            targetObject.SetActive(false);
        }
        else
        {
            // If the Toggle is unchecked, you can enable the target GameObject here if needed
        }
    }

    // Make sure to clean up the event subscription when the script is disabled
    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }
}
