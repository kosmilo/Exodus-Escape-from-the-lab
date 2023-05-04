using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivitySettings : MonoBehaviour
{
    [SerializeField] Slider mouseSensSlider;
    PlayerMovement playerMovement;


    private void Start() {

        // Find the player movement script and add a method from it as a listener
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null) {
            mouseSensSlider.onValueChanged.AddListener(playerMovement.SetMouseSens);
        }

        mouseSensSlider.value = SettingsData.mouseSens;
    }
    
    // Save the slider value to player prefs when this object is disabled
    private void OnDisable() {
        SettingsData.mouseSens = mouseSensSlider.value;
    }
}
