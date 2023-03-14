using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivitySettings : MonoBehaviour
{
    [SerializeField] Slider mouseSensSlider;
    PlayerMovement playerMovement;

    // Key for player prefs
    const string SENS_KEY = "mouseSenstivity";

    private void Start() {
        // Find the player movement script and add a method from it as a listener
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null) {
            mouseSensSlider.onValueChanged.AddListener(playerMovement.SetMouseSens);
        }

        // Set the slider value 
        mouseSensSlider.value = PlayerPrefs.GetFloat(SENS_KEY, 5f);
    }
    
    // Save the slider value to player prefs when this object is disabled
    private void OnDisable() {
        PlayerPrefs.SetFloat(SENS_KEY, mouseSensSlider.value);
    }
}
