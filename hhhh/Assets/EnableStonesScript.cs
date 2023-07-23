using UnityEngine;
using UnityEngine.UI;

public class EnableStoneScript : MonoBehaviour
{
    public Toggle toggle; // The toggle to check for enabling the game object
    public GameObject targetObject; // The game object to enable

    private void Start()
    {
        // Ensure that the toggle is not null
        if (toggle == null)
        {
            Debug.LogError("Toggle is not assigned to the script in the Inspector!");
            return;
        }

        // Ensure that the targetObject is not null
        if (targetObject == null)
        {
            Debug.LogError("Target Object is not assigned to the script in the Inspector!");
            return;
        }

        // Subscribe to the toggle's onValueChanged event
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        // Enable or disable the targetObject based on the toggle's state
        targetObject.SetActive(isOn);
    }
}
